Goal: each `Widget` records its creation time. Add `DateTimeOffset CreatedAt` as the third member of the `Widget` record, set to `DateTimeOffset.UtcNow` in the `new Widget(...)` call inside `WidgetStore.Add`.
Files to change: `src/FeatureFlow.Example.Api/Program.cs` (the `Widget` record declaration, add the `CreatedAt` member) and `src/FeatureFlow.Example.Api/WidgetStore.cs` (`Add` passes `DateTimeOffset.UtcNow`).
Tests to add: new `tests/FeatureFlow.Example.Api.UnitTests/WidgetCreatedAtTests.cs`, asserting that a widget returned by `WidgetStore.Add` has `CreatedAt` within one second of `DateTimeOffset.UtcNow`.

## Design

Changes:

- `Program.cs`: `Widget` becomes `public record Widget(int Id, string Name, DateTimeOffset CreatedAt);`. `CreatedAt` is the third positional parameter, as the ticket specifies. Because the record is serialized in the `GET /widgets`, `GET /widgets/{id}`, and `POST /widgets` responses, each response gains a `createdAt` field.
- `WidgetStore.cs`: `Add` calls `new Widget(_nextId++, name, DateTimeOffset.UtcNow)`. The clock is read inside the existing `lock`, next to the ID assignment, so the two values are taken in the same critical section. No other part of the store changes.
- `tests/FeatureFlow.Example.Api.UnitTests/WidgetCreatedAtTests.cs` (new): one test, `Add_sets_created_at_to_now`. It calls `Add`, then asserts that `Math.Abs((DateTimeOffset.UtcNow - widget.CreatedAt).TotalSeconds) < 1`. The test uses the same `[Trait("Category", "unit")]` and xUnit style as `WidgetStoreTests.cs`.

Not verified: the grep for other `new Widget(` call sites was denied, and `tests/FeatureFlow.Example.Api.IntegrationTests/WidgetRepositoryTests.cs` was not read. Any other constructor call of `Widget` with two arguments will fail to compile after the change and must also pass a `CreatedAt`.
