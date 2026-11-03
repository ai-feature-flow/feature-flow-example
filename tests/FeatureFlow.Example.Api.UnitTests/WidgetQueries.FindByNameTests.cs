using FeatureFlow.Example.Api;
using Xunit;

namespace FeatureFlow.Example.Api.UnitTests;

[Trait("Category", "unit")]
public sealed class WidgetStoreFindByNameTests
{
    [Fact]
    public void Find_by_name_matches_ignoring_case()
    {
        var store = new WidgetStore();
        var alpha = store.Add("Alpha");
        store.Add("beta");

        Assert.Equal(alpha, store.FindByName("alpha"));
        Assert.Equal(alpha, store.FindByName("ALPHA"));
        Assert.Equal(alpha, store.FindByName("Alpha"));
    }

    [Fact]
    public void Find_by_name_returns_null_when_no_widget_matches()
    {
        var store = new WidgetStore();
        store.Add("alpha");
        store.Add("beta");

        Assert.Null(store.FindByName("gamma"));
    }

    [Fact]
    public void Find_by_name_returns_null_on_empty_store()
    {
        var store = new WidgetStore();

        Assert.Null(store.FindByName("alpha"));
    }

    [Fact]
    public void Find_by_name_returns_first_added_duplicate()
    {
        var store = new WidgetStore();
        var first = store.Add("dup");
        var second = store.Add("DUP");

        var found = store.FindByName("Dup");

        Assert.NotNull(found);
        Assert.Equal(first.Id, found!.Id);
        Assert.NotEqual(second.Id, found.Id);
    }

    [Fact]
    public void Find_by_name_throws_for_null_name()
    {
        var store = new WidgetStore();
        store.Add("alpha");

        Assert.Throws<ArgumentNullException>(() => store.FindByName(null!));
    }
}
