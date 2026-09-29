using EduGest.Domain.Entities;
using EduGest.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduGest.Api.Controllers;

[ApiController,Route("api/attendance")]
public sealed class AttendanceController(EduGestDbContext db):ControllerBase
{
    private static readonly string[] Types=["ABSENCE","LATE"];
    public sealed record SaveRequest(Guid StudentId,Guid? SessionId,DateTime OccurredAt,string Type,string? Reason);
    [HttpGet,Authorize(Policy="Permission:attendance.read")]
    public async Task<IActionResult> GetAll(CancellationToken ct)=>Ok(await db.AttendanceEvents.AsNoTracking().OrderByDescending(x=>x.OccurredAt).ToListAsync(ct));
    [HttpGet("{id:guid}"),Authorize(Policy="Permission:attendance.read")]
    public async Task<IActionResult> Get(Guid id,CancellationToken ct)=>await db.AttendanceEvents.AsNoTracking().SingleOrDefaultAsync(x=>x.Id==id,ct) is { } x?Ok(x):NotFound();

    [HttpPost,Authorize(Policy="Permission:attendance.write")]
    public async Task<IActionResult> Create(SaveRequest r,CancellationToken ct)
    {
        var type=r.Type.Trim().ToUpperInvariant();if(!Types.Contains(type)||r.Reason?.Length>1000||!await db.Students.AnyAsync(x=>x.Id==r.StudentId,ct))return BadRequest();
        if(!await ValidSession(r.StudentId,r.SessionId,ct))return BadRequest("L'élève n'appartient pas à la classe de cette séance.");
        var x=new AttendanceEvent{Id=Guid.NewGuid(),StudentId=r.StudentId,SessionId=r.SessionId,OccurredAt=r.OccurredAt,Type=type,Reason=r.Reason?.Trim()};db.AttendanceEvents.Add(x);await db.SaveChangesAsync(ct);return CreatedAtAction(nameof(Get),new{id=x.Id},x);
    }
    [HttpPut("{id:guid}"),Authorize(Policy="Permission:attendance.write")]
    public async Task<IActionResult> Update(Guid id,SaveRequest r,CancellationToken ct)
    {
        var x=await db.AttendanceEvents.FindAsync([id],ct);if(x is null)return NotFound();var type=r.Type.Trim().ToUpperInvariant();if(!Types.Contains(type)||r.Reason?.Length>1000||!await db.Students.AnyAsync(s=>s.Id==r.StudentId,ct)||!await ValidSession(r.StudentId,r.SessionId,ct))return BadRequest();
        x.StudentId=r.StudentId;x.SessionId=r.SessionId;x.OccurredAt=r.OccurredAt;x.Type=type;x.Reason=r.Reason?.Trim();await db.SaveChangesAsync(ct);return NoContent();
    }
    [HttpDelete("{id:guid}"),Authorize(Policy="Permission:attendance.write")]
    public async Task<IActionResult> Delete(Guid id,CancellationToken ct){var x=await db.AttendanceEvents.FindAsync([id],ct);if(x is null)return NotFound();db.Remove(x);await db.SaveChangesAsync(ct);return NoContent();}

    private async Task<bool> ValidSession(Guid studentId,Guid? sessionId,CancellationToken ct)
    {
        if(sessionId is null)return true;
        var classId=await db.Sessions.Where(s=>s.Id==sessionId).Join(db.TeachingAssignments,s=>s.TeachingAssignmentId,a=>a.Id,(s,a)=>(Guid?)a.ClassId).SingleOrDefaultAsync(ct);
        return classId is not null&&await db.Enrollments.AnyAsync(e=>e.StudentId==studentId&&e.ClassId==classId,ct);
    }
}
