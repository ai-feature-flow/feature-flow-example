using FeatureFlow.Example.Api;
using Xunit;

namespace FeatureFlow.Example.Api.UnitTests;

[Trait("Category", "unit")]
public sealed class WidgetFindByNameTests
{
    [Fact]
    public void FindByName_returns_widget_with_exact_name()
    {
        var store = new WidgetStore();
        var added = store.Add("alpha");

        var found = store.FindByName("alpha");

        Assert.Same(added, found);
    }

    [Fact]
    public void FindByName_is_case_sensitive_and_returns_null_for_different_case()
    {
        var store = new WidgetStore();
        store.Add("alpha");

        var found = store.FindByName("ALPHA");

        Assert.Null(found);
    }
}
