using FeatureFlow.Example.Api;
using Xunit;

namespace FeatureFlow.Example.Api.UnitTests;

[Trait("Category", "unit")]
public sealed class WidgetCreatedAtTests
{
    [Fact]
    public void Add_sets_CreatedAt_within_a_second_of_now()
    {
        var store = new WidgetStore();

        var before = DateTimeOffset.UtcNow;
        var widget = store.Add("alpha");
        var after = DateTimeOffset.UtcNow;

        Assert.InRange(widget.CreatedAt, before.AddSeconds(-1), after.AddSeconds(1));
    }
}
