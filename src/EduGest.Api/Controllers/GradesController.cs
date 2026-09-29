using EduGest.Domain.Entities;
using EduGest.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduGest.Api.Controllers;

[ApiController,Route("api/grades")]
public sealed class GradesController(EduGestDbContext db):ControllerBase
{
    public sealed record SaveRequest(Guid AssessmentId,Guid StudentId,decimal Score,string? Comment);
    [HttpGet,Authorize(Policy="Permission:grades.read")]
    public async Task<IActionResult> GetAll(CancellationToken ct)=>Ok(await db.Grades.AsNoTracking().ToListAsync(ct));
    [HttpGet("{id:guid}"),Authorize(Policy="Permission:grades.read")]
    public async Task<IActionResult> Get(Guid id,CancellationToken ct)=>await db.Grades.AsNoTracking().SingleOrDefaultAsync(x=>x.Id==id,ct) is { } x?Ok(x):NotFound();

    [HttpPost,Authorize(Policy="Permission:grades.write")]
    public async Task<IActionResult> Create(SaveRequest r,CancellationToken ct)
    {
        var assessment=await db.Assessments.AsNoTracking().SingleOrDefaultAsync(x=>x.Id==r.AssessmentId,ct);if(assessment is null)return BadRequest("Évaluation inconnue.");
        if(r.Score<0||r.Score>assessment.MaxScore||r.Comment?.Length>1000)return BadRequest("Note hors barème.");
        var classId=await db.TeachingAssignments.Where(x=>x.Id==assessment.TeachingAssignmentId).Select(x=>x.ClassId).SingleAsync(ct);
        if(!await db.Enrollments.AnyAsync(x=>x.StudentId==r.StudentId&&x.ClassId==classId,ct))return BadRequest("L'élève n'est pas inscrit dans la classe de cette évaluation.");
        if(await db.Grades.AnyAsync(x=>x.AssessmentId==r.AssessmentId&&x.StudentId==r.StudentId,ct))return Conflict("Une note existe déjà.");
        var x=new Grade{Id=Guid.NewGuid(),AssessmentId=r.AssessmentId,StudentId=r.StudentId,Score=r.Score,Comment=r.Comment?.Trim()};
        db.Grades.Add(x);await db.SaveChangesAsync(ct);return CreatedAtAction(nameof(Get),new{id=x.Id},x);
    }
    [HttpPut("{id:guid}"),Authorize(Policy="Permission:grades.write")]
    public async Task<IActionResult> Update(Guid id,SaveRequest r,CancellationToken ct)
    {
        var x=await db.Grades.FindAsync([id],ct);if(x is null)return NotFound();var a=await db.Assessments.AsNoTracking().SingleOrDefaultAsync(y=>y.Id==r.AssessmentId,ct);if(a is null||r.Score<0||r.Score>a.MaxScore||r.Comment?.Length>1000)return BadRequest();
        var classId=await db.TeachingAssignments.Where(y=>y.Id==a.TeachingAssignmentId).Select(y=>y.ClassId).SingleAsync(ct);
        if(!await db.Enrollments.AnyAsync(y=>y.StudentId==r.StudentId&&y.ClassId==classId,ct))return BadRequest();
        if(await db.Grades.AnyAsync(y=>y.Id!=id&&y.AssessmentId==r.AssessmentId&&y.StudentId==r.StudentId,ct))return Conflict();
        x.AssessmentId=r.AssessmentId;x.StudentId=r.StudentId;x.Score=r.Score;x.Comment=r.Comment?.Trim();await db.SaveChangesAsync(ct);return NoContent();
    }
    [HttpDelete("{id:guid}"),Authorize(Policy="Permission:grades.write")]
    public async Task<IActionResult> Delete(Guid id,CancellationToken ct){var x=await db.Grades.FindAsync([id],ct);if(x is null)return NotFound();db.Remove(x);await db.SaveChangesAsync(ct);return NoContent();}
}
