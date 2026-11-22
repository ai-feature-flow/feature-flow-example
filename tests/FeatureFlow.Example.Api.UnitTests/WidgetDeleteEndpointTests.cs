using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace FeatureFlow.Example.Api.UnitTests;

[Trait("Category", "unit")]
public sealed class WidgetDeleteEndpointTests
{
    [Fact]
    public async Task Delete_existing_widget_returns_204_and_then_get_is_404()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var created = await client.PostAsJsonAsync("/widgets", new { name = "alpha" });
        var widget = await created.Content.ReadFromJsonAsync<Widget>();

        var response = await client.DeleteAsync($"/widgets/{widget!.Id}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, (await client.GetAsync($"/widgets/{widget.Id}")).StatusCode);
    }

    [Fact]
    public async Task Delete_unknown_widget_returns_404_and_leaves_others()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        (await client.PostAsJsonAsync("/widgets", new { name = "alpha" })).EnsureSuccessStatusCode();

        var response = await client.DeleteAsync("/widgets/999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await client.GetAsync("/widgets/1")).StatusCode);
    }
}
