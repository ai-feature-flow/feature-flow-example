Goal: add `WidgetStore.Contains(int id)`, which returns true when the store holds a widget with that id.
File to change: src/FeatureFlow.Example.Api/WidgetStore.cs
Test to add: in tests/FeatureFlow.Example.Api.UnitTests/WidgetStoreTests.cs, a test that Contains returns true after Add and false for an unknown id.
