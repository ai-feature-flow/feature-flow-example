using FeatureFlow.Example.Api;
using Xunit;

namespace FeatureFlow.Example.Api.UnitTests;

[Trait("Category", "unit")]
public sealed class WidgetCountingTests
{
    [Fact]
    public void CountByPrefix_counts_only_names_starting_with_prefix()
    {
        var store = new WidgetStore();
        store.Add("alpha");
        store.Add("alto");
        store.Add("beta");
        store.Add("ALPHA"); // differs only in case: must not match "al"

        Assert.Equal(2, store.CountByPrefix("al"));
    }

    [Fact]
    public void CountByPrefix_on_empty_store_returns_zero()
    {
        var store = new WidgetStore();

        Assert.Equal(0, store.CountByPrefix("al"));
        Assert.Equal(0, store.CountByPrefix(""));
    }

    [Fact]
    public void CountByPrefix_with_empty_prefix_counts_every_widget()
    {
        var store = new WidgetStore();
        store.Add("alpha");
        store.Add("beta");
        store.Add("gamma");

        Assert.Equal(store.Count(), store.CountByPrefix(""));
        Assert.Equal(3, store.CountByPrefix(""));
    }

    [Fact]
    public void CountByPrefix_with_null_prefix_throws_ArgumentNullException()
    {
        var store = new WidgetStore();
        store.Add("alpha");

        Assert.Throws<ArgumentNullException>(() => store.CountByPrefix(null!));
    }
}
