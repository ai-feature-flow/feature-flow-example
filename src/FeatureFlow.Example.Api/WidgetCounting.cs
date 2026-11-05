namespace FeatureFlow.Example.Api;

/// <summary>
/// Read-only query helpers over <see cref="WidgetStore"/>. Callers write
/// <c>store.CountByPrefix(...)</c>. Built only on the store's public
/// <see cref="WidgetStore.All"/>, so the store's lock and state are untouched.
/// </summary>
public static class WidgetQueries
{
    /// <summary>
    /// Number of widgets whose name starts with <paramref name="prefix"/>
    /// (ordinal, case-sensitive). An empty prefix matches every widget.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="store"/> or <paramref name="prefix"/> is null.
    /// </exception>
    public static int CountByPrefix(this WidgetStore store, string prefix)
    {
        ArgumentNullException.ThrowIfNull(store);
        ArgumentNullException.ThrowIfNull(prefix);

        return store.All().Count(w => w.Name.StartsWith(prefix, StringComparison.Ordinal));
    }
}
