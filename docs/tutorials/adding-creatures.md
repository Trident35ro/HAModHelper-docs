# Adding New Entities & Creatures

## Not Yet Available

There is currently no `HAModHelper` API for registering new creatures or AI-driven entities — the `HAModHelper.GamePlugin.Entities` module exists in the source tree (`Entities.Systems.cs`, `Entities.Interfaces.cs`, `Entities.Events.cs`) but has no implementation yet. If you're looking for how to spawn a custom animal with its own AI/behavior, that isn't possible through HAModHelper today.

This page will be filled in once an Entities API ships.

---

## What You *Can* Do Today

If what you actually need is a **custom static object** placed in the world (not a living creature) — for example, a decorative prop, a resource node, or a placeable structure — that's covered by items instead:

* [Creating Custom Items](creating-items.md) — define the item.
* [Creating Custom Items: Registering 3D World Prefabs](creating-items.md#step-2-registering-3d-world-prefabs) — attach a custom `AssetBundle` model to it via `WorldPrefabManager`.

That gives you a custom 3D object that can be dropped, placed, and picked back up like any other item — it just won't move or act on its own.

---

## Following Progress

Check the [Contributing & Collaborating](../collaborating.md) page if you're interested in helping build out the Entities API, or the [Hybrid Animals Modding Discord](https://discord.gg/TYEhNVmXhw) for updates on when this lands.
