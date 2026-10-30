Goal: add `WidgetStore.Contains(int id)`, which returns true when the store holds a widget with that id.
File to change: src/FeatureFlow.Example.Api/WidgetStore.cs
Test to add: in tests/FeatureFlow.Example.Api.UnitTests/WidgetStoreTests.cs, a test that Contains returns true after Add and false for an unknown id.

## Design

Signature: `public bool Contains(int id)` on `WidgetStore`.

Returns `true` if a widget with the given `Id` is in the store, otherwise `false`. It does not throw for unknown ids.

Why on `WidgetStore`: the store owns the widget list and its lock. `Find(int id)` already scans that list under `_gate`. `Contains` answers the same question without returning the widget, so it belongs beside `Find` and uses the same locking pattern. Callers outside the store then do not need to read the list themselves.
