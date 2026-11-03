namespace FeatureFlow.Example.Api;

/// <summary>Read-only prefix query over <see cref="WidgetStore"/>.</summary>
public static class WidgetStoreCountByPrefix
{
    /// <summary>
    /// Counts the widgets whose name starts with <paramref name="prefix"/>, matched ordinally and
    /// case-sensitively. An empty prefix matches every widget. Works on one snapshot from
    /// <see cref="WidgetStore.All"/>.
    /// </summary>
    public static int CountByPrefix(this WidgetStore store, string prefix)
    {
        ArgumentNullException.ThrowIfNull(store);
        ArgumentNullException.ThrowIfNull(prefix);

        return store.All().Count(w => w.Name.StartsWith(prefix, StringComparison.Ordinal));
    }
}
