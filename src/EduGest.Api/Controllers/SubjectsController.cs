using EduGest.Domain.Entities;
using EduGest.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduGest.Api.Controllers;

[ApiController]
[Route("api/subjects")]
public sealed class SubjectsController(EduGestDbContext db) : ControllerBase
{
    public sealed record SaveRequest(string Name, string Code);

    [HttpGet, Authorize(Policy = "Permission:subjects.read")]
    public async Task<IActionResult> GetAll(CancellationToken ct) => Ok(await db.Subjects.AsNoTracking().OrderBy(x => x.Name).ToListAsync(ct));

    [HttpGet("{id:guid}"), Authorize(Policy = "Permission:subjects.read")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct) => await db.Subjects.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id, ct) is { } x ? Ok(x) : NotFound();

    [HttpPost, Authorize(Policy = "Permission:subjects.write")]
    public async Task<IActionResult> Create(SaveRequest request, CancellationToken ct)
    {
        var name = request.Name.Trim(); var code = request.Code.Trim().ToUpperInvariant();
        if (name.Length is < 1 or > 150 || code.Length is < 1 or > 30) return BadRequest();
        if (await db.Subjects.AnyAsync(x => x.Code == code, ct)) return Conflict("Ce code matière existe déjà.");
        var entity = new Subject { Id = Guid.NewGuid(), Name = name, Code = code };
        db.Subjects.Add(entity); await db.SaveChangesAsync(ct); return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
    }

    [HttpPut("{id:guid}"), Authorize(Policy = "Permission:subjects.write")]
    public async Task<IActionResult> Update(Guid id, SaveRequest request, CancellationToken ct)
    {
        var entity = await db.Subjects.FindAsync([id], ct); if (entity is null) return NotFound();
        var name = request.Name.Trim(); var code = request.Code.Trim().ToUpperInvariant();
        if (name.Length is < 1 or > 150 || code.Length is < 1 or > 30) return BadRequest();
        if (await db.Subjects.AnyAsync(x => x.Id != id && x.Code == code, ct)) return Conflict();
        entity.Name = name; entity.Code = code; await db.SaveChangesAsync(ct); return NoContent();
    }

    [HttpDelete("{id:guid}"), Authorize(Policy = "Permission:subjects.write")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var entity = await db.Subjects.FindAsync([id], ct); if (entity is null) return NotFound();
        if (await db.TeachingAssignments.AnyAsync(x => x.SubjectId == id, ct)) return Conflict("Cette matière est utilisée par une affectation.");
        db.Remove(entity); await db.SaveChangesAsync(ct); return NoContent();
    }
}
