Goal: add `WidgetStore.Contains(int id)`, which returns true if a widget with that id is held by the store, otherwise false.
File to change: src/FeatureFlow.Example.Api/WidgetStore.cs
Test to add: tests/FeatureFlow.Example.Api.UnitTests/WidgetStoreTests.cs, covering a stored id (true) and a missing id (false).
