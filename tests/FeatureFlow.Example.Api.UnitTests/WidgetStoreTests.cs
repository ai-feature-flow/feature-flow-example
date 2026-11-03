using FeatureFlow.Example.Api;
using Xunit;

namespace FeatureFlow.Example.Api.UnitTests;

[Trait("Category", "unit")]
public sealed class WidgetStoreTests
{
    [Fact]
    public void Add_assigns_sequential_ids()
    {
        var store = new WidgetStore();

        var first = store.Add("alpha");
        var second = store.Add("beta");

        Assert.Equal(1, first.Id);
        Assert.Equal(2, second.Id);
    }

    [Fact]
    public void Find_returns_null_for_unknown_id()
    {
        var store = new WidgetStore();

        Assert.Null(store.Find(404));
    }

    [Fact]
    public void All_reflects_added_widgets()
    {
        var store = new WidgetStore();
        store.Add("alpha");

        var all = store.All();

        Assert.Single(all);
        Assert.Equal("alpha", all[0].Name);
    }

    [Fact]
    public void MaxNameLength_is_100()
    {
        Assert.Equal(100, WidgetStore.MaxNameLength);
    }

    [Fact]
    public void Add_refuses_a_101_character_name()
    {
        var store = new WidgetStore();
        var name = new string('a', 101);

        Assert.Throws<ArgumentException>(() => store.Add(name));
    }

    [Fact]
    public void Add_accepts_a_100_character_name()
    {
        var store = new WidgetStore();
        var name = new string('a', 100);

        var widget = store.Add(name);

        Assert.Equal(name, widget.Name);
    }
}
