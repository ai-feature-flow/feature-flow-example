namespace FeatureFlow.Example.Api;

/// <summary>Read-only lookup over <see cref="WidgetStore"/>, built on its <c>All()</c> snapshot.</summary>
public static class WidgetStoreFindByName
{
    /// <summary>
    /// Returns the first widget, in insertion order, whose name equals <paramref name="name"/>
    /// ignoring case, or null when no widget matches.
    /// </summary>
    public static Widget? FindByName(this WidgetStore store, string name)
    {
        ArgumentNullException.ThrowIfNull(store);
        ArgumentNullException.ThrowIfNull(name);

        return store.All().FirstOrDefault(w => string.Equals(w.Name, name, StringComparison.OrdinalIgnoreCase));
    }
}
