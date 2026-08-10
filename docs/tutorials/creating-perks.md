# Creating Perks & Abilities

This tutorial covers registering custom perks (active/passive abilities) using `HAModHelper`.

---

## Overview

Perks live in the `HAModHelper.GamePlugin.Perks.Systems` namespace and follow the same `Model` + `Manager` pattern as items: you build a `Perk` object and hand it to `PerkManager.Instance`.

### Perk Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| `ModId` | `string` | Your mod's unique identifier (e.g., `"MyMod"`). Defaults to `"base"` if left unset — always set this explicitly for your own perks. |
| `PerkId` | `string` | The perk's identifier (e.g., `"Fireball"`). |
| `Id` | `string` | Read-only. Computed as `"{ModId}:{PerkId}"`. |
| `Name` | `string` | **Required.** In-game display name. |
| `Description` | `string` | **Required.** Short description shown in perk lists. |
| `DetailedDescription` | `string` | **Required.** Longer description shown when the perk is inspected. |
| `UltraDetailedDescription` | `string?` | Optional. Extended tooltip text for advanced UI, if the base game shows it. |
| `SpritePath` | `string?` | Perk icon. Same resolution rules as items — see [Sprite Resolution](creating-items.md#sprite-resolution). |
| `PerkEffects` | `Dictionary<string, SinglePerkEffect>?` | Optional, advanced. See [Perk Effects](#perk-effects-advanced) below. |

---

## Known Issue: Registering Perks at Startup

> [!WARNING]
> **`PerkManager.Instance` currently creates a brand-new `PerkManager` on every access**, unlike `ItemManager.Instance` (a true singleton). Combined with the fact that nothing in HAModHelper ever calls `PerkManager.ProcessQueuedPerks()`, this means: if you call `AddPerk(...)` before the game's `PerkControl` object exists in the scene (e.g. directly inside your plugin's `Load()`), **the perk is silently lost** — there is no retry and no error.
>
> Until this is fixed, register perks using the retry pattern below instead of a single `AddPerk` call.

```csharp
using BepInEx.Unity.IL2CPP;
using HAModHelper.GamePlugin.Perks.Systems;

namespace MyMod;

public class Plugin : BasePlugin
{
    public override void Load()
    {
        var fireball = new Perk
        {
            ModId = "MyMod",
            PerkId = "Fireball",
            Name = "Fireball",
            Description = "Hurl a ball of fire at your enemies.",
            DetailedDescription = "Deals fire damage in a small radius on impact.",
        };

        // Fire-and-forget: keeps retrying in the background until it registers.
        _ = RegisterPerkUntilItSticks(fireball);
    }

    // Workaround for the PerkManager bug described above: retries AddPerk on a
    // timer and stops as soon as GetPerk confirms it actually stuck, instead of
    // relying on PerkManager's (currently non-functional) internal queue.
    private static async Task RegisterPerkUntilItSticks(Perk perk, int maxAttempts = 60)
    {
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            PerkManager.Instance.AddPerk(perk);

            if (PerkManager.Instance.GetPerk(perk.Id) != null)
                return; // PerkControl was available this time - registered for good.

            await Task.Delay(1000);
        }
    }
}
```

<!--
DROP-IN REPLACEMENT — once PerkManager.Instance is a true singleton and its queue is
flushed automatically (mirroring how ItemManager already works), delete the
[!WARNING] block and the RegisterPerkUntilItSticks example above and replace this
whole section with the text below:

## Registering a Perk

Call `PerkManager.Instance.AddPerk(...)` from your plugin's `Load()` method — like
items, this queues the perk internally and HAModHelper flushes that queue
automatically once the game is ready for it, so no delay or retry is needed.

```csharp
using BepInEx.Unity.IL2CPP;
using HAModHelper.GamePlugin.Perks.Systems;

namespace MyMod;

public class Plugin : BasePlugin
{
    public override void Load()
    {
        var fireball = new Perk
        {
            ModId = "MyMod",
            PerkId = "Fireball",
            Name = "Fireball",
            Description = "Hurl a ball of fire at your enemies.",
            DetailedDescription = "Deals fire damage in a small radius on impact.",
        };

        PerkManager.Instance.AddPerk(fireball);
    }
}
```
-->

---

## Perk Effects (Advanced)

`PerkEffects` lets you attach mechanical behavior (damage, duration, area, etc.) to a perk. Each entry is a `SinglePerkEffect` — a game-defined type (`Il2Cpp.SinglePerkEffect`, not part of HAModHelper) that just wraps a raw `Dictionary<string, string>` of configuration:

```csharp
using HAModHelper.GamePlugin.Helpers;

var effectData = new Dictionary<string, string>
{
    ["type"] = "Instant_effect",
    // ...remaining keys depend on the effect type.
};

var effect = new SinglePerkEffect(DictHelper.DenormalizeIL2CPPDictionary(effectData));

fireball.PerkEffects = new()
{
    ["OnCast"] = effect,
};
```

HAModHelper doesn't document or validate the key schema inside `SinglePerkEffect` — it's whatever the base game's own perk system expects, and it likely differs per effect type. The most reliable way to find the right keys for the effect you want is to fetch an existing vanilla perk and inspect its data:

```csharp
Perk? vanillaPerk = PerkManager.Instance.GetPerk("base:SomeVanillaPerkId");
// Inspect vanillaPerk?.PerkEffects to see the key/value shape for that effect type.
```

If you don't need custom mechanical behavior, you can safely leave `PerkEffects` unset — a perk with no effects still registers, displays, and can be cast.

---

## Modifying or Deleting Existing Perks

```csharp
using HAModHelper.GamePlugin.Perks.Systems;

Perk? perk = PerkManager.Instance.GetPerk("base:SomePerkId");
if (perk != null)
{
    perk.Description = "A rebalanced version of this perk.";

    // Unlike Item, Perk has no UpdateItem() convenience method — call PatchPerk directly.
    PerkManager.Instance.PatchPerk(perk);
}

// Remove a perk entirely from the game registry
Perk? unwantedPerk = PerkManager.Instance.GetPerk("base:UnwantedPerkId");
if (unwantedPerk != null)
{
    PerkManager.Instance.DeletePerk(unwantedPerk);
}
```

> [!WARNING]
> `GetPerk` can return `null` even for a valid ID if the game's underlying `PerkControl` object isn't loaded yet — same caveat as `ItemManager.GetItem`. Always null-check the result.

---

## Next Steps

* [Using the Event Bus](event-bus.md) — `PerkCastEvent` is defined for perk-cast hooks, though it isn't fired by HAModHelper internally yet; the Event Bus itself is fully usable for your own custom events today.
