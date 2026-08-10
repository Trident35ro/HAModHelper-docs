# Using the Event Bus

`HAModHelper` ships a small, generic publish/subscribe system called `EventBus`. It's useful for reacting to things happening elsewhere in your mod (or another mod) without those two pieces of code needing to call each other directly.

---

## Overview

Everything lives in the `HAModHelper.GamePlugin.Base.Events` namespace:

* **`EventBus.Instance`:** A single shared bus. Since it's exposed by the `HAModHelper.GamePlugin` assembly that every mod references, all mods subscribing to or firing the same event type talk to each other through the same bus — this works for cross-mod communication, not just within your own mod.
* **`BaseEvent`:** The class every event type must inherit from.

---

## Defining a Custom Event

Subclass `BaseEvent` and add whatever data your event needs:

```csharp
using HAModHelper.GamePlugin.Base.Events;

public sealed class PlayerCraftedItemEvent : BaseEvent
{
    public string ItemFullId { get; }

    public PlayerCraftedItemEvent(string itemFullId)
    {
        ItemFullId = itemFullId;
    }
}
```

`BaseEvent` gives every event three properties:

| Property | Description |
| :--- | :--- |
| `Handled` | Not enforced by `EventBus` itself — a convention for handlers to signal "I dealt with this" to other handlers or to whoever fired the event. Set it yourself if your event needs the concept. |
| `Cancelled` | If a handler sets this to `true`, `EventBus` stops calling any remaining handlers for that `Fire` call. |
| `Fired` | Set automatically by `EventBus`. Read-only from outside the bus. |

---

## Subscribing

```csharp
using HAModHelper.GamePlugin.Base.Events;

IDisposable subscription = EventBus.Instance.Subscribe<PlayerCraftedItemEvent>(ev =>
{
    Log.LogInfo($"Player crafted: {ev.ItemFullId}");
});
```

`Subscribe<TEvent>` returns an `IDisposable`. Call `.Dispose()` on it when you want to stop listening — this is optional for handlers that should live for the whole session (most plugin-level handlers registered in `Load()` fall into this category), but matters if you're subscribing from something shorter-lived.

---

## Firing an Event

```csharp
var ev = new PlayerCraftedItemEvent("MyMod:EtheriteOre");
EventBus.Instance.Fire(ev);

if (ev.Cancelled)
{
    // A handler asked for this to be cancelled - respect that here.
}
```

* Handlers run in subscription order, and a handler that sets `Cancelled = true` stops any handlers registered after it from running.
* **Each event instance can only be fired once.** Calling `Fire` a second time with the same instance throws an exception — construct a new instance for each occurrence instead of reusing one.
* If nothing is subscribed to `TEvent`, `Fire` is a cheap no-op and `Fired` stays `false` on the returned event.

---

## Built-in Events

HAModHelper currently ships one predefined event type, `PerkCastEvent` (`HAModHelper.GamePlugin.Perks.Events`), carrying a `PerkId`. It exists as a reserved hook for perk-cast handling, but **nothing in HAModHelper fires it yet** — don't build logic that expects it to trigger automatically. The `EventBus` itself is fully functional today for any custom event types you define, as shown above.
