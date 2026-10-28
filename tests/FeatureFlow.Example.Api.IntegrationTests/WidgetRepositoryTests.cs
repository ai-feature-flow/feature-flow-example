using Npgsql;
using Xunit;

namespace FeatureFlow.Example.Api.IntegrationTests;

/// <summary>
/// The Docker-dependent tier. Runs against a real Postgres, either through
/// Testcontainers locally or through CI delegation in GitHub Actions.
/// </summary>
[Trait("Category", "integration")]
public sealed class WidgetRepositoryTests : IAsyncLifetime
{
    private readonly string _connectionString =
        Environment.GetEnvironmentVariable("ConnectionStrings__Default")
        ?? "Host=localhost;Port=5432;Username=ff;Password=ff;Database=ff_example";

    public async Task InitializeAsync()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = new NpgsqlCommand(
            """
            CREATE TABLE IF NOT EXISTS widgets (
                id   serial PRIMARY KEY,
                name text NOT NULL
            );
            """,
            connection);
        await command.ExecuteNonQueryAsync();
    }

    public async Task DisposeAsync()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = new NpgsqlCommand("DROP TABLE IF EXISTS widgets;", connection);
        await command.ExecuteNonQueryAsync();
    }

    [Fact]
    public async Task Insert_then_read_round_trips()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var insert = new NpgsqlCommand(
            "INSERT INTO widgets (name) VALUES (@name) RETURNING id;", connection);
        insert.Parameters.AddWithValue("name", "round-trip");
        var id = (int)(await insert.ExecuteScalarAsync())!;

        await using var read = new NpgsqlCommand(
            "SELECT name FROM widgets WHERE id = @id;", connection);
        read.Parameters.AddWithValue("id", id);
        var name = (string)(await read.ExecuteScalarAsync())!;

        Assert.Equal("round-trip", name);
    }

    [Fact]
    public async Task Database_reports_its_version()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = new NpgsqlCommand("SELECT version();", connection);
        var version = (string)(await command.ExecuteScalarAsync())!;

        Assert.Contains("PostgreSQL", version, StringComparison.Ordinal);
    }
}
