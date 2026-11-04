using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using FeatureFlow.Example.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace FeatureFlow.Example.Api.UnitTests;

[Trait("Category", "unit")]
public sealed class WidgetCountEndpointTests
{
    [Fact]
    public async Task Count_is_zero_on_an_empty_store()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        await AssertCountAsync(client, 0);
    }

    [Fact]
    public async Task Count_reflects_widgets_added_through_post()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        await AssertCountAsync(client, 0);

        await AddWidgetAsync(client, "alpha");
        await AssertCountAsync(client, 1);

        await AddWidgetAsync(client, "beta");
        await AssertCountAsync(client, 2);
    }

    private static async Task AddWidgetAsync(HttpClient client, string name)
    {
        using var response = await client.PostAsJsonAsync("/widgets", new { name });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    private static async Task AssertCountAsync(HttpClient client, int expected)
    {
        using var response = await client.GetAsync("/widgets/count");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);

        var body = await response.Content.ReadAsStringAsync();
        using var document = JsonDocument.Parse(body);

        Assert.Equal(expected, document.RootElement.GetProperty("count").GetInt32());
    }
}
