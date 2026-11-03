using FeatureFlow.Example.Api;
using Xunit;

namespace FeatureFlow.Example.Api.UnitTests;

[Trait("Category", "unit")]
public sealed class WidgetStoreCountByPrefixTests
{
    [Fact]
    public void CountByPrefix_counts_only_names_starting_with_prefix()
    {
        var store = new WidgetStore();
        store.Add("alpha");
        store.Add("alto");
        store.Add("beta");

        Assert.Equal(2, store.CountByPrefix("al"));
    }

    [Fact]
    public void CountByPrefix_is_case_sensitive()
    {
        var store = new WidgetStore();
        store.Add("alpha");
        store.Add("alto");
        store.Add("beta");

        Assert.Equal(0, store.CountByPrefix("Al"));
    }

    [Fact]
    public void CountByPrefix_returns_zero_for_non_matching_prefix()
    {
        var store = new WidgetStore();
        store.Add("alpha");
        store.Add("alto");
        store.Add("beta");

        Assert.Equal(0, store.CountByPrefix("zeta"));
    }

    [Fact]
    public void CountByPrefix_empty_prefix_matches_every_widget()
    {
        var store = new WidgetStore();
        store.Add("alpha");
        store.Add("alto");
        store.Add("beta");

        Assert.Equal(store.Count(), store.CountByPrefix(""));
        Assert.Equal(3, store.CountByPrefix(""));
    }

    [Fact]
    public void CountByPrefix_null_prefix_throws_ArgumentNullException()
    {
        var store = new WidgetStore();

        Assert.Throws<ArgumentNullException>(() => store.CountByPrefix(null!));
    }

    [Fact]
    public void CountByPrefix_null_store_throws_ArgumentNullException()
    {
        WidgetStore store = null!;

        Assert.Throws<ArgumentNullException>(() => store.CountByPrefix("al"));
    }

    [Fact]
    public void CountByPrefix_empty_store_returns_zero()
    {
        var store = new WidgetStore();

        Assert.Equal(0, store.CountByPrefix("al"));
    }

    [Fact]
    public void CountByPrefix_empty_store_returns_zero_for_empty_prefix()
    {
        var store = new WidgetStore();

        Assert.Equal(0, store.CountByPrefix(""));
    }
}
