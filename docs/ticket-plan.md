Goal: add `WidgetStore.Contains(int id)`, which returns true when the store holds a widget with that id.
File to change: src/FeatureFlow.Example.Api/WidgetStore.cs
Test to add: tests/FeatureFlow.Example.Api.UnitTests/WidgetStoreTests.cs, asserting `Contains` is true for an added widget's id and false for an unknown id.
