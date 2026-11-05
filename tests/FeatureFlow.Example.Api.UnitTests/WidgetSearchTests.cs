using FeatureFlow.Example.Api;
using Xunit;

namespace FeatureFlow.Example.Api.UnitTests;

[Trait("Category", "unit")]
public sealed class WidgetSearchTests
{
    [Fact]
    public void FindByName_returns_widget_ignoring_case()
    {
        var store = new WidgetStore();
        store.Add("Gamma");
        var alpha = store.Add("Alpha");
        store.Add("beta");

        var found = store.FindByName("alpha");

        Assert.NotNull(found);
        Assert.Equal(alpha.Id, found.Id);
        Assert.Equal("Alpha", found.Name);
    }

    [Fact]
    public void FindByName_returns_null_when_no_widget_has_the_name()
    {
        var store = new WidgetStore();
        store.Add("alpha");
        store.Add("beta");

        Assert.Null(store.FindByName("gamma"));
    }

    [Fact]
    public void FindByName_on_empty_store_returns_null()
    {
        var store = new WidgetStore();

        Assert.Null(store.FindByName("alpha"));
    }

    [Fact]
    public void FindByName_returns_earliest_added_widget_for_repeated_names()
    {
        var store = new WidgetStore();
        var first = store.Add("Alpha");
        store.Add("ALPHA");

        var found = store.FindByName("alpha");

        Assert.NotNull(found);
        Assert.Equal(first.Id, found.Id);
    }
}
