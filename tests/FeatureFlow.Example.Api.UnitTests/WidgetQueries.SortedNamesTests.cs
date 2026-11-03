using FeatureFlow.Example.Api;
using Xunit;

namespace FeatureFlow.Example.Api.UnitTests;

[Trait("Category", "unit")]
public sealed class WidgetStoreSortedNamesTests
{
    [Fact]
    public void Sorted_names_are_returned_in_ordinal_order()
    {
        var store = new WidgetStore();
        store.Add("gamma");
        store.Add("alpha");
        store.Add("beta");

        var names = store.SortedNames();

        Assert.Equal(new[] { "alpha", "beta", "gamma" }, names);
    }

    [Fact]
    public void Sorted_names_use_ordinal_case_sensitive_comparison()
    {
        var store = new WidgetStore();
        store.Add("alpha");
        store.Add("Beta");

        var names = store.SortedNames();

        // Ordinal: uppercase letters sort before lowercase ones.
        Assert.Equal(new[] { "Beta", "alpha" }, names);
    }

    [Fact]
    public void Empty_store_returns_empty_list()
    {
        var store = new WidgetStore();

        var names = store.SortedNames();

        Assert.NotNull(names);
        Assert.Empty(names);
    }

    [Fact]
    public void Duplicate_names_are_kept_once_per_widget_and_adjacent()
    {
        var store = new WidgetStore();
        store.Add("dup");
        store.Add("beta");
        store.Add("dup");

        var names = store.SortedNames();

        Assert.Equal(new[] { "beta", "dup", "dup" }, names);
    }
}
