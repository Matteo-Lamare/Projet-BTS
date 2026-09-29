using EduGest.Domain.Entities;
using EduGest.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduGest.Api.Controllers;

[ApiController,Route("api/enrollments")]
public sealed class EnrollmentsController(EduGestDbContext db):ControllerBase
{
    public sealed record CreateRequest(Guid StudentId,Guid ClassId);
    [HttpGet,Authorize(Policy="Permission:students.read")]
    public async Task<IActionResult> GetAll(CancellationToken ct)=>Ok(await db.Enrollments.AsNoTracking().ToListAsync(ct));
    [HttpPost,Authorize(Policy="Permission:students.write")]
    public async Task<IActionResult> Create(CreateRequest r,CancellationToken ct)
    {
        if(!await db.Students.AnyAsync(x=>x.Id==r.StudentId,ct)||!await db.Classes.AnyAsync(x=>x.Id==r.ClassId,ct))return BadRequest();
        if(await db.Enrollments.AnyAsync(x=>x.StudentId==r.StudentId&&x.ClassId==r.ClassId,ct))return Conflict();
        var e=new Enrollment{Id=Guid.NewGuid(),StudentId=r.StudentId,ClassId=r.ClassId};db.Enrollments.Add(e);await db.SaveChangesAsync(ct);return Created("",e);
    }
    [HttpDelete("{id:guid}"),Authorize(Policy="Permission:students.write")]
    public async Task<IActionResult> Delete(Guid id,CancellationToken ct){var x=await db.Enrollments.FindAsync([id],ct);if(x is null)return NotFound();db.Remove(x);await db.SaveChangesAsync(ct);return NoContent();}
}
