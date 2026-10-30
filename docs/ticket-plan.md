Goal: add WidgetStore.Contains(int id) so callers can check whether a widget with that id is held by the store.
File to change: src/FeatureFlow.Example.Api/WidgetStore.cs
Test to add: tests/FeatureFlow.Example.Api.UnitTests/WidgetStoreTests.cs

## Design

Add `public bool Contains(int id)` to `WidgetStore` (`src/FeatureFlow.Example.Api/WidgetStore.cs`).

- Returns `true` if the store holds a widget whose `Id` equals `id`, otherwise `false`.
- Runs under the same `_gate` lock as `Find` and `Count`, and matches on the same `Id` field `Find` uses.
- Belongs on `WidgetStore` because the store owns the widgets and their ids. Callers that only need an existence check would otherwise call `Find` and test the result for null, which exposes the `Widget` for no reason.
