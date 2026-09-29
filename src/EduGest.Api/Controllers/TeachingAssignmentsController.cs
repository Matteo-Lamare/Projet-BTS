using EduGest.Domain.Entities;
using EduGest.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduGest.Api.Controllers;

[ApiController,Route("api/teaching-assignments")]
public sealed class TeachingAssignmentsController(EduGestDbContext db):ControllerBase
{
    public sealed record CreateRequest(Guid TeacherId,Guid ClassId,Guid SubjectId);
    [HttpGet,Authorize(Policy="Permission:teachers.read")]
    public async Task<IActionResult> GetAll(CancellationToken ct)=>Ok(await db.TeachingAssignments.AsNoTracking().ToListAsync(ct));
    [HttpPost,Authorize(Policy="Permission:teachers.write")]
    public async Task<IActionResult> Create(CreateRequest r,CancellationToken ct)
    {
        if(!await db.Teachers.AnyAsync(x=>x.Id==r.TeacherId,ct)||!await db.Classes.AnyAsync(x=>x.Id==r.ClassId,ct)||!await db.Subjects.AnyAsync(x=>x.Id==r.SubjectId,ct))return BadRequest();
        if(await db.TeachingAssignments.AnyAsync(x=>x.TeacherId==r.TeacherId&&x.ClassId==r.ClassId&&x.SubjectId==r.SubjectId,ct))return Conflict();
        var e=new TeachingAssignment{Id=Guid.NewGuid(),TeacherId=r.TeacherId,ClassId=r.ClassId,SubjectId=r.SubjectId};db.TeachingAssignments.Add(e);await db.SaveChangesAsync(ct);return Created("",e);
    }
    [HttpDelete("{id:guid}"),Authorize(Policy="Permission:teachers.write")]
    public async Task<IActionResult> Delete(Guid id,CancellationToken ct){var x=await db.TeachingAssignments.FindAsync([id],ct);if(x is null)return NotFound();if(await db.Assessments.AnyAsync(a=>a.TeachingAssignmentId==id,ct)||await db.Sessions.AnyAsync(s=>s.TeachingAssignmentId==id,ct))return Conflict("Cette affectation possède des données scolaires.");db.Remove(x);await db.SaveChangesAsync(ct);return NoContent();}
}
