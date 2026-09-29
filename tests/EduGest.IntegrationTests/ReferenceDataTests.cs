using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace EduGest.IntegrationTests;

public sealed class ReferenceDataTests : IClassFixture<AuthApiFactory>
{
    private readonly AuthApiFactory _factory;
    public ReferenceDataTests(AuthApiFactory factory) => _factory = factory;

    [Fact]
    public async Task Administrator_can_create_academic_year_class_and_subject()
    {
        await _factory.EnsureUserAsync("Administrator");
        var client = await _factory.LoginAsync();

        var yearResponse = await client.PostAsJsonAsync("/api/academic-years", new { label = "2026-2027", startDate = new DateTime(2026, 9, 1), endDate = new DateTime(2027, 7, 5) });
        Assert.Equal(HttpStatusCode.Created, yearResponse.StatusCode);
        var year = await yearResponse.Content.ReadFromJsonAsync<AcademicYearResponse>();
        Assert.NotNull(year);

        var classResponse = await client.PostAsJsonAsync("/api/classes", new { name = "BTS SIO 1", academicYearId = year!.Id });
        Assert.Equal(HttpStatusCode.Created, classResponse.StatusCode);

        var subjectResponse = await client.PostAsJsonAsync("/api/subjects", new { name = "Développement", code = "DEV" });
        Assert.Equal(HttpStatusCode.Created, subjectResponse.StatusCode);
    }

    [Fact]
    public async Task Student_cannot_create_subject()
    {
        await _factory.EnsureUserAsync("Student");
        var client = await _factory.LoginAsync();
        var response = await client.PostAsJsonAsync("/api/subjects", new { name = "Interdite", code = "NOPE" });
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    private sealed record AcademicYearResponse(Guid Id, string Label, DateTime StartDate, DateTime EndDate);
}
