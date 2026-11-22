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
    public void Remove_deletes_present_widget_and_reports_absent_one()
    {
        var store = new WidgetStore();
        var widget = store.Add("alpha");
        store.Add("beta");

        Assert.True(store.Remove(widget.Id));
        Assert.Null(store.Find(widget.Id));
        Assert.Equal(1, store.Count());
        Assert.False(store.Remove(widget.Id));
        Assert.False(store.Remove(404));
    }

    [Fact]
    public async Task Concurrent_removals_of_the_same_id_succeed_exactly_once()
    {
        var store = new WidgetStore();
        var widget = store.Add("alpha");

        var results = await Task.WhenAll(
            Enumerable.Range(0, 32).Select(_ => Task.Run(() => store.Remove(widget.Id))));

        Assert.Equal(1, results.Count(r => r));
        Assert.Equal(0, store.Count());
    }

    [Fact]
    public void Count_reports_held_widgets()
    {
        var store = new WidgetStore();

        Assert.Equal(0, store.Count());

        store.Add("alpha");
        Assert.Equal(1, store.Count());

        store.Add("beta");
        Assert.Equal(2, store.Count());
    }
}
