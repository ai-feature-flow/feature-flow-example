using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace FeatureFlow.Example.Api.UnitTests;

/// <summary>
/// HTTP-level tests for GET /widgets/count, run in-memory against the real host.
/// Each test builds its own host so that its WidgetStore starts empty.
/// </summary>
[Trait("Category", "unit")]
public sealed class WidgetCountEndpointTests
{
    [Fact]
    public async Task Count_on_empty_store_is_a_json_object_with_count_zero()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/widgets/count");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);

        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(JsonValueKind.Object, json.RootElement.ValueKind);

        var property = Assert.Single(json.RootElement.EnumerateObject());
        Assert.Equal("count", property.Name);
        Assert.Equal(JsonValueKind.Number, property.Value.ValueKind);
        Assert.Equal(0, property.Value.GetInt32());
    }

    [Fact]
    public async Task Count_matches_widgets_posted_through_the_api()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        Assert.Equal(0, await ReadCountAsync(client));

        (await client.PostAsJsonAsync("/widgets", new { name = "alpha" })).EnsureSuccessStatusCode();
        (await client.PostAsJsonAsync("/widgets", new { name = "beta" })).EnsureSuccessStatusCode();

        Assert.Equal(2, await ReadCountAsync(client));
    }

    private static async Task<int> ReadCountAsync(HttpClient client)
    {
        using var json = JsonDocument.Parse(await client.GetStringAsync("/widgets/count"));
        return json.RootElement.GetProperty("count").GetInt32();
    }
}
