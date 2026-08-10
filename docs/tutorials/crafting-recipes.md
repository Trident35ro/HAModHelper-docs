# Injecting Crafting Recipes

This tutorial covers making your items craftable at vanilla crafting stations using `CraftingInjectionManager`.

> [!NOTE]
> This is a separate step from creating the item itself — see [Creating Custom Items](creating-items.md) first if you haven't registered your item yet.

---

## How It Works

`CraftingInjectionManager` lives in the `HAModHelper.GamePlugin.Items.Systems` namespace (it's part of the Items module, not a separate one). It works by patching the game's `GetCraftList` method once, then replaying every registered injection whenever any mod's craft list is requested — so multiple mods can inject into the same list safely, and reopening the crafting menu repeatedly won't create duplicate entries.

```csharp
using HAModHelper.GamePlugin.Items.Systems;

// Example A: Append Etherite Ore to the bottom of the basic Crafting Table list
CraftingInjectionManager.Instance.Inject(
    craftListName: "Crafting - Crafting Table",
    itemFullId: "MyMod:EtheriteOre"
);

// Example B: Insert Etherite Ore into the Crucible list directly after Iron Ingot
CraftingInjectionManager.Instance.Inject(
    craftListName: "Crafting - Crucible",
    itemFullId: "MyMod:EtheriteOre",
    insertAfter: "base:IronIngot"
);
```

Call `Inject(...)` once — from your plugin's `Load()` method is fine, since this just registers an entry in a list; the actual insertion happens lazily the next time the game builds that craft list.

### Parameters

| Parameter | Description |
| :--- | :--- |
| `craftListName` | The craft list's file name, **without** an extension (e.g. `"Crafting - Crafting Table"`, `"Crafting - Crucible"`). This must match the base game's internal list name exactly — there's no built-in list of valid names, so you'll need to find them by inspecting the game's own craft list files or observing `craft_list_file_name` values via logging. |
| `itemFullId` | The full ID of the item to inject, in `"ModId:ItemId"` form for your own items, or `"base:ItemName"` for vanilla items. |
| `insertAfter` | Optional. The full ID of an existing entry to insert directly after. Omit (or pass `null`) to append at the end of the list instead. |

> [!NOTE]
> **[SCREENSHOT PLACEHOLDER: Crafting Menu Injection]**
> * **Description:** Insert a screenshot showing the crafting interface with the new item listed as craftable.
> * **Recommended Focus:** Highlight the recipe slot inside the Crucible or Crafting Table UI.
>
> <p align="center">
>   <img src="../../resources/item_crafting_injection.png" alt="Crafting Menu Injected Recipe"/>
> </p>

---

## Notes & Limitations

* Injection is a pure UI-list insertion — it does not define what ingredients are required to craft the item. Recipe/ingredient data comes from the item's own game fields (not currently modeled by `HAModHelper.Items.Systems.Item`); if you need a custom ingredient list, you'll need to set the relevant raw fields via `Item.ExtraFields`.
* `insertAfter` is matched by scanning the *current* list, which already includes any earlier injections from your mod or others — so injection order between mods can affect the final position when several mods target the same anchor item.
* If `insertAfter` doesn't match any entry currently in the list, your item is silently appended at the end instead of failing.
