using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace EduGest.IntegrationTests;

public sealed class GradesTests : IClassFixture<AuthApiFactory>
{
    private readonly AuthApiFactory _factory;
    public GradesTests(AuthApiFactory factory)=>_factory=factory;

    [Fact]
    public async Task Administrator_can_create_assessment_and_valid_grade_but_reject_over_maximum()
    {
        await _factory.EnsureUserAsync("Administrator");
        var client=await _factory.LoginAsync();
        var suffix=Guid.NewGuid().ToString("N")[..8];

        var year=await Entity(client,"/api/academic-years",new{label=$"G-{suffix}",startDate=new DateTime(2032,9,1),endDate=new DateTime(2033,7,1)});
        var cls=await Entity(client,"/api/classes",new{name=$"C-{suffix}",academicYearId=year});
        var subject=await Entity(client,"/api/subjects",new{name=$"Subject {suffix}",code=$"G{suffix}"});
        var student=await Entity(client,"/api/students",new{userName=$"gs.{suffix}",email=$"gs.{suffix}@example.test",password="TestOnly-1Aa!",firstName="A",lastName="B",birthDate=(DateTime?)null});
        var teacher=await Entity(client,"/api/teachers",new{userName=$"gt.{suffix}",email=$"gt.{suffix}@example.test",password="TestOnly-1Aa!",firstName="C",lastName="D"});
        await client.PostAsJsonAsync("/api/enrollments",new{studentId=student,classId=cls});
        var assignment=await Entity(client,"/api/teaching-assignments",new{teacherId=teacher,classId=cls,subjectId=subject});
        var assessment=await Entity(client,"/api/assessments",new{teachingAssignmentId=assignment,title="Contrôle",assessmentDate=new DateTime(2032,10,1),maxScore=20m});

        Assert.Equal(HttpStatusCode.Created,(await client.PostAsJsonAsync("/api/grades",new{assessmentId=assessment,studentId=student,score=15.5m,comment="Bon travail"})).StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest,(await client.PostAsJsonAsync("/api/grades",new{assessmentId=assessment,studentId=Guid.NewGuid(),score=25m,comment=(string?)null})).StatusCode);
    }

    private static async Task<Guid> Entity(HttpClient client,string url,object body)
    {
        var response=await client.PostAsJsonAsync(url,body);response.EnsureSuccessStatusCode();
        var entity=await response.Content.ReadFromJsonAsync<EntityResponse>();return entity!.Id;
    }
    private sealed record EntityResponse(Guid Id);
}
