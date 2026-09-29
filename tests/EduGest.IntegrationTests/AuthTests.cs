using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using EduGest.Infrastructure.Identity;
using EduGest.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit;

namespace EduGest.IntegrationTests;

public sealed class AuthTests : IClassFixture<AuthApiFactory>
{
    private readonly AuthApiFactory _factory;
    public AuthTests(AuthApiFactory factory) => _factory = factory;

    [Fact]
    public async Task Login_then_me_returns_authenticated_user()
    {
        await _factory.EnsureUserAsync();
        var client = _factory.CreateClient();
        var login = await client.PostAsJsonAsync("/api/auth/login", new { userName = "integration.admin", password = "ValidPassword1!" });
        login.EnsureSuccessStatusCode();
        var tokens = await login.Content.ReadFromJsonAsync<TokenResponse>();
        Assert.NotNull(tokens);

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens!.AccessToken);
        var me = await client.GetAsync("/api/auth/me");
        Assert.Equal(HttpStatusCode.OK, me.StatusCode);
    }

    [Fact]
    public async Task Refresh_rotates_token_and_old_token_is_rejected()
    {
        await _factory.EnsureUserAsync();
        var client = _factory.CreateClient();
        var login = await client.PostAsJsonAsync("/api/auth/login", new { userName = "integration.admin", password = "ValidPassword1!" });
        var first = await login.Content.ReadFromJsonAsync<TokenResponse>();
        Assert.NotNull(first);

        var refreshed = await client.PostAsJsonAsync("/api/auth/refresh", new { refreshToken = first!.RefreshToken });
        refreshed.EnsureSuccessStatusCode();

        var reuse = await client.PostAsJsonAsync("/api/auth/refresh", new { refreshToken = first.RefreshToken });
        Assert.Equal(HttpStatusCode.Unauthorized, reuse.StatusCode);
    }

    [Fact]
    public async Task Permission_endpoint_returns_401_without_token()
    {
        var response = await _factory.CreateClient().GetAsync("/api/authorization/users-manage");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Permission_endpoint_returns_403_without_permission()
    {
        await _factory.EnsureUserAsync();
        var client = await _factory.LoginAsync();
        var response = await client.GetAsync("/api/authorization/users-manage");
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task Administrator_can_access_users_manage_permission()
    {
        await _factory.EnsureUserAsync("Administrator");
        var client = await _factory.LoginAsync();
        var response = await client.GetAsync("/api/authorization/users-manage");
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    private sealed record TokenResponse(string AccessToken, string RefreshToken);
}

public sealed class AuthApiFactory : WebApplicationFactory<Program>
{
    private SqliteConnection? _connection;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.UseSetting("ConnectionStrings:DefaultConnection", "Host=unused");
        builder.UseSetting("Jwt:Issuer", "EduGest.Tests");
        builder.UseSetting("Jwt:Audience", "EduGest.Tests");
        builder.UseSetting("Jwt:SigningKey", "integration-tests-signing-key-at-least-32-characters-long");

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<EduGestDbContext>>();
            services.RemoveAll<EduGestDbContext>();
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
            services.AddDbContext<EduGestDbContext>(o => o.UseSqlite(_connection));
        });
    }

    public async Task<HttpClient> LoginAsync()
    {
        var client = CreateClient();
        var login = await client.PostAsJsonAsync("/api/auth/login", new { userName = "integration.admin", password = "ValidPassword1!" });
        login.EnsureSuccessStatusCode();
        var tokens = await login.Content.ReadFromJsonAsync<TokenPair>();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens!.AccessToken);
        return client;
    }

    public async Task EnsureUserAsync(string? role = null)
    {
        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<EduGestDbContext>();
        await db.Database.EnsureCreatedAsync();
        var manager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        await IdentitySeed.SeedAsync(db, roleManager, migrate: false);
        if (await manager.FindByNameAsync("integration.admin") is null)
        {
            var user = new ApplicationUser { Id = Guid.NewGuid(), UserName = "integration.admin", Email = "integration@example.test" };
            var result = await manager.CreateAsync(user, "ValidPassword1!");
            Assert.True(result.Succeeded, string.Join("; ", result.Errors.Select(x => x.Description)));
        }

        if (role is not null)
        {
            var user = await manager.FindByNameAsync("integration.admin");
            Assert.NotNull(user);
            var current = await manager.GetRolesAsync(user!);
            if (current.Count > 0) await manager.RemoveFromRolesAsync(user!, current);
            await manager.AddToRoleAsync(user!, role);
        }
    }

    private sealed record TokenPair(string AccessToken, string RefreshToken);

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing) _connection?.Dispose();
    }
}
