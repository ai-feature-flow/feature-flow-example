using FeatureFlow.Example.Api;
using Xunit;

namespace FeatureFlow.Example.Api.UnitTests;

[Trait("Category", "unit")]
public sealed class WidgetNameNormalisationTests
{
    [Fact]
    public void Add_trims_and_lower_cases_name()
    {
        var store = new WidgetStore();

        var widget = store.Add("  Alpha ");

        Assert.Equal("alpha", widget.Name);
    }
}
