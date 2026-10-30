Goal: add `WidgetStore.Contains(int id)`, which returns true when the store holds a widget with that id.
File to change: src/FeatureFlow.Example.Api/WidgetStore.cs
Test to add: tests/FeatureFlow.Example.Api.UnitTests/WidgetStoreTests.cs, asserting `Contains` is true for an added widget's id and false for an unknown id.

## Design

Signature: `public bool Contains(int id)` on `WidgetStore` (`src/FeatureFlow.Example.Api/WidgetStore.cs`).

Behaviour: returns `true` if `_widgets` holds a widget whose `Id` equals `id`, otherwise `false`. It takes the `_gate` lock, as `Find` and `Count` do, and does not mutate state.

Placement: `WidgetStore` already owns the widget list and the id lookup (`Find(int id)`). `Contains` is a boolean view of the same lookup, so it belongs beside `Find` and shares its locking. Placing it elsewhere would require callers to reach into the store's list or duplicate the lock.
