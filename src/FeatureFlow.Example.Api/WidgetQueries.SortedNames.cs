namespace FeatureFlow.Example.Api;

/// <summary>Read-only listing over <see cref="WidgetStore"/>, built on its <c>All()</c> snapshot.</summary>
public static class WidgetStoreSortedNames
{
    /// <summary>
    /// Returns the name of every widget, sorted with <see cref="StringComparer.Ordinal"/>.
    /// Duplicate names are kept, once per widget. An empty store returns an empty list.
    /// </summary>
    public static IReadOnlyList<string> SortedNames(this WidgetStore store)
    {
        ArgumentNullException.ThrowIfNull(store);

        return store.All()
            .OrderBy(w => w.Name, StringComparer.Ordinal)
            .Select(w => w.Name)
            .ToList();
    }
}
