using FeatureFlow.Example.Api;
using Xunit;

namespace FeatureFlow.Example.Api.UnitTests;

[Trait("Category", "unit")]
public sealed class WidgetNamesTests
{
    [Fact]
    public void SortedNames_returns_names_in_ordinal_order()
    {
        var store = new WidgetStore();
        store.Add("gamma");
        store.Add("alpha");
        store.Add("beta");

        var names = store.SortedNames();

        Assert.Equal(new[] { "alpha", "beta", "gamma" }, names);
    }

    [Fact]
    public void SortedNames_uses_ordinal_not_culture_order_and_keeps_duplicates()
    {
        var store = new WidgetStore();
        store.Add("banana");
        store.Add("apple");
        store.Add("Banana");
        store.Add("Apple");
        store.Add("apple");

        var names = store.SortedNames();

        // Ordinal: uppercase letters sort before lowercase, and both "apple"s are kept.
        Assert.Equal(new[] { "Apple", "Banana", "apple", "apple", "banana" }, names);
    }

    [Fact]
    public void SortedNames_on_empty_store_returns_empty_list()
    {
        var store = new WidgetStore();

        var names = store.SortedNames();

        Assert.NotNull(names);
        Assert.Empty(names);
    }
}
