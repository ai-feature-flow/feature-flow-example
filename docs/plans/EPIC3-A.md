Goal: WidgetStore.Add stores widget names trimmed and lower-cased (invariant culture), via a new private static Normalise(string) used in the new Widget(...) expression.
Files: src/FeatureFlow.Example.Api/WidgetStore.cs (add Normalise, apply it in Add); tests/FeatureFlow.Example.Api.UnitTests/WidgetNameNormalisationTests.cs (new).
Tests: WidgetNameNormalisationTests.cs, Add("  Alpha ") returns a widget named "alpha".

## Design

Widget names are stored trimmed and lower-cased. The change touches three members.

- `WidgetStore.Normalise(string name)`: new, `private static`, in `src/FeatureFlow.Example.Api/WidgetStore.cs`. Returns `name.Trim().ToLowerInvariant()`. The invariant culture makes the result independent of the host locale (for example, Turkish `I`/`ı`).
- `WidgetStore.Add(string name)`: changed. The constructor call becomes `new Widget(_nextId++, Normalise(name))`. The signature does not change. Normalisation runs inside the existing `_gate` lock, so no new synchronisation is needed.
- `WidgetNameNormalisationTests`: new test class in `tests/FeatureFlow.Example.Api.UnitTests/WidgetNameNormalisationTests.cs`. It asserts that `Add("  Alpha ")` returns a widget whose name is `"alpha"`.

Unchanged: the `Widget` type, `All`, `Find`, `Count`, and `Program.cs`, which passes `request.Name` to `Add` without modification. The existing `WidgetStoreTests.cs` calls `Add("alpha")` and `Add("beta")`, which are already normalised, so its assertions are not affected.
