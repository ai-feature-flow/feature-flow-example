Goal: WidgetStore.Add stores widget names trimmed and lower-cased (invariant culture), via a new private static Normalise(string) used in the new Widget(...) expression.
Files: src/FeatureFlow.Example.Api/WidgetStore.cs (add Normalise, apply it in Add); tests/FeatureFlow.Example.Api.UnitTests/WidgetNameNormalisationTests.cs (new).
Tests: WidgetNameNormalisationTests.cs, Add("  Alpha ") returns a widget named "alpha".
