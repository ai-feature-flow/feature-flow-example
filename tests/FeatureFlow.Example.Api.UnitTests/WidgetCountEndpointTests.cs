using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace FeatureFlow.Example.Api.UnitTests;

/// <summary>
/// Exercises GET /widgets/count over HTTP in-process, against the real API pipeline.
/// Each test builds its own host, so each starts with an empty WidgetStore singleton.
/// </summary>
[Trait("Category", "unit")]
public sealed class WidgetCountEndpointTests
{
    [Fact]
    public async Task Count_route_answers_200_JSON_with_zero_for_empty_store()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/widgets/count");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
        Assert.Equal(0, await ReadCountBodyAsync(response));
    }

    [Fact]
    public async Task Count_route_answers_200_JSON_with_count_when_store_holds_widgets()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        await AddWidgetAsync(client, "alpha");
        await AddWidgetAsync(client, "beta");

        var response = await client.GetAsync("/widgets/count");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
        Assert.Equal(2, await ReadCountBodyAsync(response));
    }

    [Fact]
    public async Task Count_route_value_follows_store_as_widgets_are_added()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        Assert.Equal(0, await ReadCountAsync(client));

        await AddWidgetAsync(client, "alpha");
        Assert.Equal(1, await ReadCountAsync(client));

        await AddWidgetAsync(client, "beta");
        Assert.Equal(2, await ReadCountAsync(client));
    }

    private static async Task AddWidgetAsync(HttpClient client, string name)
    {
        var response = await client.PostAsJsonAsync("/widgets", new { name });
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    private static async Task<int> ReadCountAsync(HttpClient client)
    {
        var response = await client.GetAsync("/widgets/count");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        return await ReadCountBodyAsync(response);
    }

    // Checks that the body is an object whose only property is "count", an integer, and returns it.
    private static async Task<int> ReadCountBodyAsync(HttpResponseMessage response)
    {
        using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var root = document.RootElement;

        Assert.Equal(JsonValueKind.Object, root.ValueKind);
        var properties = root.EnumerateObject().ToList();
        Assert.Single(properties);
        Assert.Equal("count", properties[0].Name);
        Assert.Equal(JsonValueKind.Number, properties[0].Value.ValueKind);

        return properties[0].Value.GetInt32();
    }
}
