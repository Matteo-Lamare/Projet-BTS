using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace EduGest.IntegrationTests;

public sealed class AttendanceTests : IClassFixture<AuthApiFactory>
{
    private readonly AuthApiFactory _factory;
    public AttendanceTests(AuthApiFactory factory)=>_factory=factory;

    [Fact]
    public async Task Administrator_can_create_session_and_absence_for_enrolled_student()
    {
        await _factory.EnsureUserAsync("Administrator");var client=await _factory.LoginAsync();var n=Guid.NewGuid().ToString("N")[..8];
        var year=await Entity(client,"/api/academic-years",new{label=$"A-{n}",startDate=new DateTime(2034,9,1),endDate=new DateTime(2035,7,1)});
        var cls=await Entity(client,"/api/classes",new{name=$"C-{n}",academicYearId=year});
        var subject=await Entity(client,"/api/subjects",new{name=$"Subject {n}",code=$"A{n}"});
        var student=await Entity(client,"/api/students",new{userName=$"as.{n}",email=$"as.{n}@example.test",password="TestOnly-1Aa!",firstName="A",lastName="B",birthDate=(DateTime?)null});
        var teacher=await Entity(client,"/api/teachers",new{userName=$"at.{n}",email=$"at.{n}@example.test",password="TestOnly-1Aa!",firstName="C",lastName="D"});
        (await client.PostAsJsonAsync("/api/enrollments",new{studentId=student,classId=cls})).EnsureSuccessStatusCode();
        var assignment=await Entity(client,"/api/teaching-assignments",new{teacherId=teacher,classId=cls,subjectId=subject});
        var session=await Entity(client,"/api/sessions",new{teachingAssignmentId=assignment,roomId=(Guid?)null,startsAt=new DateTime(2034,10,2,8,0,0),endsAt=new DateTime(2034,10,2,10,0,0)});
        var absence=await client.PostAsJsonAsync("/api/attendance",new{studentId=student,sessionId=session,occurredAt=new DateTime(2034,10,2,8,0,0),type="ABSENCE",reason="Test"});
        Assert.Equal(HttpStatusCode.Created,absence.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest,(await client.PostAsJsonAsync("/api/attendance",new{studentId=student,sessionId=session,occurredAt=new DateTime(2034,10,2,8,0,0),type="UNKNOWN",reason=(string?)null})).StatusCode);
    }

    private static async Task<Guid> Entity(HttpClient client,string url,object body){var r=await client.PostAsJsonAsync(url,body);r.EnsureSuccessStatusCode();return (await r.Content.ReadFromJsonAsync<EntityResponse>())!.Id;}
    private sealed record EntityResponse(Guid Id);
}
