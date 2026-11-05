namespace FeatureFlow.Example.Api;

/// <summary>
/// Read-only lookups over a <see cref="WidgetStore"/>. Built only on the store's
/// public <c>All()</c> snapshot, so the store's lock and state are untouched.
/// </summary>
public static class WidgetQueries
{
    /// <summary>
    /// Returns the earliest-added widget whose name equals <paramref name="name"/>
    /// ignoring case, or null when none does.
    /// </summary>
    public static Widget? FindByName(this WidgetStore store, string name)
    {
        ArgumentNullException.ThrowIfNull(store);
        ArgumentNullException.ThrowIfNull(name);

        // All() returns widgets in insertion order, so FirstOrDefault gives the earliest-added match.
        return store.All().FirstOrDefault(w =>
            string.Equals(w.Name, name, StringComparison.OrdinalIgnoreCase));
    }
}
