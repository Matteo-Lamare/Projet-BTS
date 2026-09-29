using EduGest.Domain.Entities;
using EduGest.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduGest.Api.Controllers;

[ApiController,Route("api/sessions")]
public sealed class SessionsController(EduGestDbContext db):ControllerBase
{
    public sealed record SaveRequest(Guid TeachingAssignmentId,Guid? RoomId,DateTime StartsAt,DateTime EndsAt);
    [HttpGet,Authorize(Policy="Permission:timetable.read")]
    public async Task<IActionResult> GetAll(CancellationToken ct)=>Ok(await db.Sessions.AsNoTracking().OrderBy(x=>x.StartsAt).ToListAsync(ct));
    [HttpGet("{id:guid}"),Authorize(Policy="Permission:timetable.read")]
    public async Task<IActionResult> Get(Guid id,CancellationToken ct)=>await db.Sessions.AsNoTracking().SingleOrDefaultAsync(x=>x.Id==id,ct) is { } x?Ok(x):NotFound();
    [HttpPost,Authorize(Policy="Permission:attendance.write")]
    public async Task<IActionResult> Create(SaveRequest r,CancellationToken ct)
    {
        if(r.StartsAt>=r.EndsAt||!await db.TeachingAssignments.AnyAsync(x=>x.Id==r.TeachingAssignmentId,ct))return BadRequest();
        if(r.RoomId is not null&&!await db.Rooms.AnyAsync(x=>x.Id==r.RoomId,ct))return BadRequest("Salle inconnue.");
        var x=new Session{Id=Guid.NewGuid(),TeachingAssignmentId=r.TeachingAssignmentId,RoomId=r.RoomId,StartsAt=r.StartsAt,EndsAt=r.EndsAt};db.Sessions.Add(x);await db.SaveChangesAsync(ct);return CreatedAtAction(nameof(Get),new{id=x.Id},x);
    }
    [HttpPut("{id:guid}"),Authorize(Policy="Permission:attendance.write")]
    public async Task<IActionResult> Update(Guid id,SaveRequest r,CancellationToken ct){var x=await db.Sessions.FindAsync([id],ct);if(x is null)return NotFound();if(r.StartsAt>=r.EndsAt||!await db.TeachingAssignments.AnyAsync(a=>a.Id==r.TeachingAssignmentId,ct))return BadRequest();if(r.RoomId is not null&&!await db.Rooms.AnyAsync(a=>a.Id==r.RoomId,ct))return BadRequest();x.TeachingAssignmentId=r.TeachingAssignmentId;x.RoomId=r.RoomId;x.StartsAt=r.StartsAt;x.EndsAt=r.EndsAt;await db.SaveChangesAsync(ct);return NoContent();}
    [HttpDelete("{id:guid}"),Authorize(Policy="Permission:attendance.write")]
    public async Task<IActionResult> Delete(Guid id,CancellationToken ct){var x=await db.Sessions.FindAsync([id],ct);if(x is null)return NotFound();if(await db.AttendanceEvents.AnyAsync(a=>a.SessionId==id,ct))return Conflict("Cette séance possède des événements de présence.");db.Remove(x);await db.SaveChangesAsync(ct);return NoContent();}
}
