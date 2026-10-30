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

## Design: Contains

`public bool Contains(int id)` returns `true` if the store holds a widget
whose `Id` equals `id`, and `false` otherwise. This method does not exist in
WidgetStore.cs yet; this section records the planned signature.

It belongs on WidgetStore because the store owns `_widgets` and `_gate`.
The lookup must run under `_gate`, as `Find` and `Add` do, so a concurrent
`Add` cannot be missed or observed half-complete.
