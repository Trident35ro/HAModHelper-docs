# Getting Started with Collaborating

## Overview

We welcome contributions to HAML, `HAModHelper.GamePlugin`, and related tools. This guide outlines how `HAModHelper` works under the hood and how to contribute to the code.

---

## Architecture & API Design

`HAModHelper.GamePlugin` acts as a central library for other mods. It provides a modular API so developers can easily register items, perks, and entities without writing manual Harmony patches.

### Core Structure Pattern

The plugin uses Singleton Managers to expose functionality across systems:

* **`Base/` (Entrypoint):** `Base.GamePlugin.cs` initializes all core managers during startup (`ItemManager.Instance.Initialize()`, `PerkManager.Instance.Initialize()`, `WorldPrefabManager.Instance.Initialize()`, `CraftingInjectionManager.Instance.Initialize()`).
* **Interfaces (`*.Interfaces.cs`):** Defines contract definitions (e.g., `IResourceControl`, `IPerkControl`) that decouple manager logic from the live Unity/IL2CPP game objects. This indirection is what makes it possible to swap in a fake implementation during unit tests (see [Testing](#testing) below) instead of requiring a running game.
* **Systems (`*.Systems.cs`):** Houses the manager class itself, its public model type (e.g., `Item`, `Perk`), and a paired static `*Converter` class (e.g., `ItemConverter`, `PerkConverter`) that translates between that model and the game's raw field data.
* **Managers:** Public singletons exposing methods to external mods (e.g., `ItemManager.Instance.AddItem(...)`).

---

### Manager Pattern in Practice

`ItemManager` and `PerkManager` both follow the same internal shape, so once you understand one you understand the other:

| Method | Purpose |
| :--- | :--- |
| `AddItem` / `AddPerk` | Stores the object in an internal dictionary keyed by full ID, then attempts to push it into the game's live cache immediately via `TryInjectIntoGameCache`. |
| `DeleteItem` / `DeletePerk` | Removes the object from the internal dictionary and the game cache. If it's a `base:` entry, the ID is also recorded as blocked so `IsBaseItemBlocked` / `IsBasePerkBlocked` can report it. |
| `PatchItem` / `PatchPerk` | Implemented as a delete followed by an add — there's no separate "update in place" code path. |
| `GetItem` / `GetPerk` | Checks the internal dictionary first, then falls back to asking the game's live `IResourceControl` / `IPerkControl` and converting the result back into a model via the relevant `*Converter`. Returns `null` if the control object isn't available yet. |
| `TryInjectIntoGameCache` | If the game's control object isn't reachable yet, the entry is placed in an internal queue instead of failing outright. |
| `ProcessQueuedItems` / `ProcessQueuedPerks` | Drains that queue. For items, this is wired into a Harmony patch on `ResourceControl.TryLoadInventoryItem` so it runs automatically the first time the game requests an item — there is currently no equivalent call site for perks, which is why perk registration needs the workaround documented in [Creating Perks](tutorials/creating-perks.md#known-issue-registering-perks-at-startup). |

If you're adding a new registrable system (e.g. a future Entities manager), matching this method set keeps behavior predictable for both modders and other contributors.

---

### Converters

Model types like `Item` and `Perk` never touch the game's raw field dictionaries directly — that's the job of their paired converter (`ItemConverter`, `PerkConverter`). Converters are stateless static classes with two directions:

* **To game fields** (`ToGameFields` / `ToPerkData`): turns a model instance into the `Dictionary<string, string>` (or `PerkData`) the game actually understands, applying any defaults for unset fields.
* **From game fields** (`FromGameFields` / `FromPerkData`): reconstructs a model instance from data read out of the game, used by `GetItem` / `GetPerk` when falling back to a vanilla entry that was never registered through HAModHelper.

Anything not explicitly modeled on `Item` or `Perk` should go through `ExtraFields` rather than being added as a new converter case, unless it's common enough to justify a first-class property.

---

## Testing

`HAModHelper.Tests` is an xUnit project that exercises manager logic without a running instance of the game. This works because every manager exposes a `Debug*ControlSource` property (`ItemManager.DebugResourceControlSource`, `PerkManager.DebugPerkControlSource`) that lets a test inject a fake `IResourceControl` / `IPerkControl` implementation in place of the real one, which is normally located at runtime via `UnityEngine.Object.FindObjectOfType`.

Since managers are singletons, tests must reset shared state between runs by calling the manager's `Reset()` method (marked test-only in its summary) — typically in the test class constructor, so each test starts from a clean slate.

When contributing a new manager or extending an existing one, add or update tests under `HAModHelper.Tests` alongside your change rather than relying on manual in-game verification alone.

---

## How to Contribute

1. **Fork the Repository:** Create a personal fork on GitHub.
2. **Clone Locally:**
   ```bash
   git clone https://github.com/<your-username>/HAModHelper.git
   ```
3. **Create a Feature Branch:**
   ```bash
   git checkout -b feature/my-new-feature
   ```
4. **Implement Your Changes:**
   * **Adding API Features:** Define the interface in `*.Interfaces.cs`, implement logic in `*.Systems.cs`, and expose it through the relevant Singleton Manager.
   * **Coding Style:** Maintain clean C# conventions and ensure all new public methods are properly documented.
   * **Tests:** Add or update coverage in `HAModHelper.Tests` (see [Testing](#testing) above) for any manager or converter logic you touch.
5. **Commit & Push:**
   ```bash
   git commit -m "Add new feature API"
   git push origin feature/my-new-feature
   ```
6. **Open a Pull Request:** Submit a PR targeting the `main` branch.