namespace FeatureFlow.Example.Api;

/// <summary>
/// Read-only name listing over <see cref="WidgetStore"/>. Callers write
/// <c>store.SortedNames()</c>. Built only on the store's public
/// <see cref="WidgetStore.All"/>, so the store's lock and state are untouched.
/// </summary>
public static class WidgetNames
{
    /// <summary>
    /// Every widget's name in ordinal sort order. Duplicate names are each listed.
    /// An empty store gives an empty list.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="store"/> is null.
    /// </exception>
    public static IReadOnlyList<string> SortedNames(this WidgetStore store)
    {
        ArgumentNullException.ThrowIfNull(store);

        return store.All()
            .Select(w => w.Name)
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToList();
    }
}
