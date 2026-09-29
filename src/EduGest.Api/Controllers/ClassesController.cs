using EduGest.Domain.Entities;
using EduGest.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduGest.Api.Controllers;

[ApiController]
[Route("api/classes")]
public sealed class ClassesController(EduGestDbContext db) : ControllerBase
{
    public sealed record Request(string Name, Guid AcademicYearId);

    [HttpGet, Authorize(Policy = "Permission:classes.read")]
    public async Task<IActionResult> GetAll(CancellationToken ct) => Ok(await db.Classes.AsNoTracking().OrderBy(x => x.Name).ToListAsync(ct));

    [HttpGet("{id:guid}"), Authorize(Policy = "Permission:classes.read")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct) => await db.Classes.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id, ct) is { } x ? Ok(x) : NotFound();

    [HttpPost, Authorize(Policy = "Permission:classes.write")]
    public async Task<IActionResult> Create(Request request, CancellationToken ct)
    {
        var name = request.Name.Trim();
        if (name.Length is < 1 or > 100) return BadRequest();
        if (!await db.AcademicYears.AnyAsync(x => x.Id == request.AcademicYearId, ct)) return BadRequest("Année scolaire inconnue.");
        if (await db.Classes.AnyAsync(x => x.Name == name && x.AcademicYearId == request.AcademicYearId, ct)) return Conflict();
        var entity = new Class { Id = Guid.NewGuid(), Name = name, AcademicYearId = request.AcademicYearId };
        db.Classes.Add(entity); await db.SaveChangesAsync(ct); return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
    }

    [HttpPut("{id:guid}"), Authorize(Policy = "Permission:classes.write")]
    public async Task<IActionResult> Update(Guid id, Request request, CancellationToken ct)
    {
        var entity = await db.Classes.FindAsync([id], ct); if (entity is null) return NotFound();
        var name = request.Name.Trim(); if (name.Length is < 1 or > 100) return BadRequest();
        if (!await db.AcademicYears.AnyAsync(x => x.Id == request.AcademicYearId, ct)) return BadRequest();
        if (await db.Classes.AnyAsync(x => x.Id != id && x.Name == name && x.AcademicYearId == request.AcademicYearId, ct)) return Conflict();
        entity.Name = name; entity.AcademicYearId = request.AcademicYearId; await db.SaveChangesAsync(ct); return NoContent();
    }

    [HttpDelete("{id:guid}"), Authorize(Policy = "Permission:classes.write")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var entity = await db.Classes.FindAsync([id], ct); if (entity is null) return NotFound();
        if (await db.Enrollments.AnyAsync(x => x.ClassId == id, ct) || await db.TeachingAssignments.AnyAsync(x => x.ClassId == id, ct))
            return Conflict("Cette classe est déjà utilisée.");
        db.Remove(entity); await db.SaveChangesAsync(ct); return NoContent();
    }
}
