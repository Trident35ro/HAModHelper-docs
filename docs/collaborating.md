# Getting Started with Collaborating

## Overview

We welcome contributions to HAML, `HAModHelper.GamePlugin`, and related tools. This guide outlines how `HAModHelper` works under the hood and how to contribute to the code.

---

## Architecture & API Design

`HAModHelper.GamePlugin` acts as a central library for other mods. It provides a modular API so developers can easily register items, perks, and entities without writing manual Harmony patches.

### Core Structure Pattern

The plugin uses Singleton Managers to expose functionality across systems:

* **`Base/` (Entrypoint):** `Base.GamePlugin.cs` initializes all core managers during startup (`ItemManager.Instance.Initialize()`, `PerkManager.Instance.Initialize()`).
* **Interfaces (`*.Interfaces.cs`):** Defines contract definitions (e.g., `IItemData`, `IPerkData`).
* **Systems (`*.Systems.cs`):** Houses system logic, state dictionary storage, and internal game hooks.
* **Managers:** Public singletons exposing methods to external mods (e.g., `ItemManager.Instance.RegisterItem(...)`).

---

## How to Contribute

1. **Fork the Repository:** Create a personal fork on GitHub.
2. **Clone Locally:**
   ```bash
   git clone [https://github.com/YourUsername/HAModHelper.git](https://github.com/YourUsername/HAModHelper.git)
   ```
3. **Create a Feature Branch:**
   ```bash
   git checkout -b feature/my-new-feature
   ```
4. **Implement Your Changes:**
   * **Adding API Features:** Define the interface in `*.Interfaces.cs`, implement logic in `*.Systems.cs`, and expose it through the relevant Singleton Manager.
   * **Coding Style:** Maintain clean C# conventions and ensure all new public methods are properly documented.
5. **Commit & Push:**
   ```bash
   git commit -m "Add new feature API"
   git push origin feature/my-new-feature
   ```
6. **Open a Pull Request:** Submit a PR targeting the `main` branch.