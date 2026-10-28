## Design

`public int Count()` returns `_widgets.Count`.
It belongs on WidgetStore because the store owns the collection and is the
only type that may read it under its lock.

# Ticket plan

Goal: expose the number of widgets held by the store.
File to change: src/FeatureFlow.Example.Api/WidgetStore.cs
Test to add: tests/FeatureFlow.Example.Api.UnitTests/WidgetStoreTests.cs

