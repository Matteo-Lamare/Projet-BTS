using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace EduGest.IntegrationTests;

public sealed class PeopleAndAssignmentsTests : IClassFixture<AuthApiFactory>
{
    private readonly AuthApiFactory _factory;
    public PeopleAndAssignmentsTests(AuthApiFactory factory) => _factory = factory;

    [Fact]
    public async Task Administrator_can_create_people_and_school_links()
    {
        await _factory.EnsureUserAsync("Administrator");
        var client = await _factory.LoginAsync();
        var suffix = Guid.NewGuid().ToString("N")[..8];

        var yearResponse = await client.PostAsJsonAsync("/api/academic-years", new { label = $"Y-{suffix}", startDate = new DateTime(2030, 9, 1), endDate = new DateTime(2031, 7, 5) });
        var year = await yearResponse.Content.ReadFromJsonAsync<EntityResponse>();
        Assert.NotNull(year);

        var classResponse = await client.PostAsJsonAsync("/api/classes", new { name = $"Class-{suffix}", academicYearId = year!.Id });
        var schoolClass = await classResponse.Content.ReadFromJsonAsync<EntityResponse>();
        Assert.NotNull(schoolClass);

        var subjectResponse = await client.PostAsJsonAsync("/api/subjects", new { name = $"Subject {suffix}", code = $"S{suffix}" });
        var subject = await subjectResponse.Content.ReadFromJsonAsync<EntityResponse>();
        Assert.NotNull(subject);

        var studentResponse = await client.PostAsJsonAsync("/api/students", new { userName = $"student.{suffix}", email = $"student.{suffix}@example.test", password = "TestOnly-1Aa!", firstName = "Alice", lastName = "Martin", birthDate = new DateTime(2007, 4, 12) });
        Assert.Equal(HttpStatusCode.Created, studentResponse.StatusCode);
        var student = await studentResponse.Content.ReadFromJsonAsync<EntityResponse>();
        Assert.NotNull(student);

        var teacherResponse = await client.PostAsJsonAsync("/api/teachers", new { userName = $"teacher.{suffix}", email = $"teacher.{suffix}@example.test", password = "TestOnly-1Aa!", firstName = "Jean", lastName = "Dupont" });
        Assert.Equal(HttpStatusCode.Created, teacherResponse.StatusCode);
        var teacher = await teacherResponse.Content.ReadFromJsonAsync<EntityResponse>();
        Assert.NotNull(teacher);

        Assert.Equal(HttpStatusCode.Created, (await client.PostAsJsonAsync("/api/enrollments", new { studentId = student!.Id, classId = schoolClass!.Id })).StatusCode);
        Assert.Equal(HttpStatusCode.Created, (await client.PostAsJsonAsync("/api/teaching-assignments", new { teacherId = teacher!.Id, classId = schoolClass.Id, subjectId = subject!.Id })).StatusCode);
    }

    private sealed record EntityResponse(Guid Id);
}
