## Design

`public int Count()` returns `_widgets.Count`.
It belongs on WidgetStore because the store owns the collection and is the
only type that may read it under its lock.

# Ticket plan

Goal: expose the number of widgets held by the store.
File to change: src/FeatureFlow.Example.Api/WidgetStore.cs
Test to add: tests/FeatureFlow.Example.Api.UnitTests/WidgetStoreTests.cs


## Design

`public int Count()` returns `_widgets.Count`.
It belongs on WidgetStore because the store owns the collection and is the
only type that may read it under its lock.

## Design

`public bool Contains(int id)` returns `true` if a widget with `Id == id` is
in the store, and `false` otherwise.
It belongs on WidgetStore because the store owns `_widgets` and `_gate`. The
check must run under the same lock as `Find` and `Count`, so callers outside
the class cannot read the collection safely.
Implemented in `WidgetStore.cs`.
