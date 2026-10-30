Goal: add `WidgetStore.Contains(int id)`, which returns true if a widget with that id is held by the store, otherwise false.
File to change: src/FeatureFlow.Example.Api/WidgetStore.cs
Test to add: tests/FeatureFlow.Example.Api.UnitTests/WidgetStoreTests.cs, covering a stored id (true) and a missing id (false).

## Design

Signature: `public bool Contains(int id)` on `WidgetStore`.

Returns `true` if a widget with the given `id` is in the store, `false` otherwise. It does not return the widget. Use `Find(int id)` when the widget itself is needed.

Why `WidgetStore`: the store owns the `_widgets` list and guards it with `_gate`. `Find` and `Count` already read that list under the lock, and `Contains` should do the same so that callers never read the list without it. Placing the check on the store also keeps the id-lookup logic in one place, so a later Postgres-backed implementation only needs to match this one method's behaviour.

