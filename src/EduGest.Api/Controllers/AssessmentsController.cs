using EduGest.Domain.Entities;
using EduGest.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduGest.Api.Controllers;

[ApiController,Route("api/assessments")]
public sealed class AssessmentsController(EduGestDbContext db):ControllerBase
{
    public sealed record SaveRequest(Guid TeachingAssignmentId,string Title,DateTime AssessmentDate,decimal MaxScore);
    [HttpGet,Authorize(Policy="Permission:grades.read")]
    public async Task<IActionResult> GetAll(CancellationToken ct)=>Ok(await db.Assessments.AsNoTracking().OrderByDescending(x=>x.AssessmentDate).ToListAsync(ct));
    [HttpGet("{id:guid}"),Authorize(Policy="Permission:grades.read")]
    public async Task<IActionResult> Get(Guid id,CancellationToken ct)=>await db.Assessments.AsNoTracking().SingleOrDefaultAsync(x=>x.Id==id,ct) is { } x?Ok(x):NotFound();

    [HttpPost,Authorize(Policy="Permission:grades.write")]
    public async Task<IActionResult> Create(SaveRequest r,CancellationToken ct)
    {
        var title=r.Title.Trim();if(title.Length is <1 or >200||r.MaxScore<=0||r.MaxScore>100)return BadRequest();
        if(!await db.TeachingAssignments.AnyAsync(x=>x.Id==r.TeachingAssignmentId,ct))return BadRequest("Affectation inconnue.");
        var x=new Assessment{Id=Guid.NewGuid(),TeachingAssignmentId=r.TeachingAssignmentId,Title=title,AssessmentDate=r.AssessmentDate,MaxScore=r.MaxScore};
        db.Assessments.Add(x);await db.SaveChangesAsync(ct);return CreatedAtAction(nameof(Get),new{id=x.Id},x);
    }
    [HttpPut("{id:guid}"),Authorize(Policy="Permission:grades.write")]
    public async Task<IActionResult> Update(Guid id,SaveRequest r,CancellationToken ct)
    {
        var x=await db.Assessments.FindAsync([id],ct);if(x is null)return NotFound();var title=r.Title.Trim();
        if(title.Length is <1 or >200||r.MaxScore<=0||r.MaxScore>100)return BadRequest();
        if(!await db.TeachingAssignments.AnyAsync(a=>a.Id==r.TeachingAssignmentId,ct))return BadRequest();
        if(await db.Grades.AnyAsync(g=>g.AssessmentId==id&&g.Score>r.MaxScore,ct))return Conflict("Le nouveau barème est inférieur à une note existante.");
        x.TeachingAssignmentId=r.TeachingAssignmentId;x.Title=title;x.AssessmentDate=r.AssessmentDate;x.MaxScore=r.MaxScore;await db.SaveChangesAsync(ct);return NoContent();
    }
    [HttpDelete("{id:guid}"),Authorize(Policy="Permission:grades.write")]
    public async Task<IActionResult> Delete(Guid id,CancellationToken ct){var x=await db.Assessments.FindAsync([id],ct);if(x is null)return NotFound();if(await db.Grades.AnyAsync(g=>g.AssessmentId==id,ct))return Conflict("Cette évaluation possède des notes.");db.Remove(x);await db.SaveChangesAsync(ct);return NoContent();}
}
