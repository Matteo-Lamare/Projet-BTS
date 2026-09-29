using EduGest.Domain.Entities;
using EduGest.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduGest.Api.Controllers;

[ApiController]
[Route("api/academic-years")]
public sealed class AcademicYearsController(EduGestDbContext db) : ControllerBase
{
    public sealed record Request(string Label, DateTime StartDate, DateTime EndDate);

    [HttpGet, Authorize(Policy = "Permission:classes.read")]
    public async Task<IActionResult> GetAll(CancellationToken ct) =>
        Ok(await db.AcademicYears.AsNoTracking().OrderByDescending(x => x.StartDate).ToListAsync(ct));

    [HttpPost, Authorize(Policy = "Permission:classes.write")]
    public async Task<IActionResult> Create(Request request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Label) || request.StartDate >= request.EndDate)
            return ValidationProblem("Le libellé est obligatoire et la date de début doit précéder la date de fin.");
        if (await db.AcademicYears.AnyAsync(x => x.Label == request.Label.Trim(), ct))
            return Conflict("Une année scolaire avec ce libellé existe déjà.");
        var entity = new AcademicYear { Id = Guid.NewGuid(), Label = request.Label.Trim(), StartDate = request.StartDate, EndDate = request.EndDate };
        db.AcademicYears.Add(entity); await db.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
    }

    [HttpGet("{id:guid}"), Authorize(Policy = "Permission:classes.read")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct) =>
        await db.AcademicYears.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id, ct) is { } x ? Ok(x) : NotFound();

    [HttpPut("{id:guid}"), Authorize(Policy = "Permission:classes.write")]
    public async Task<IActionResult> Update(Guid id, Request request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Label) || request.StartDate >= request.EndDate) return BadRequest();
        var entity = await db.AcademicYears.FindAsync([id], ct); if (entity is null) return NotFound();
        if (await db.AcademicYears.AnyAsync(x => x.Id != id && x.Label == request.Label.Trim(), ct)) return Conflict();
        entity.Label = request.Label.Trim(); entity.StartDate = request.StartDate; entity.EndDate = request.EndDate;
        await db.SaveChangesAsync(ct); return NoContent();
    }

    [HttpDelete("{id:guid}"), Authorize(Policy = "Permission:classes.write")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var entity = await db.AcademicYears.FindAsync([id], ct); if (entity is null) return NotFound();
        if (await db.Classes.AnyAsync(x => x.AcademicYearId == id, ct)) return Conflict("Cette année scolaire est utilisée par une classe.");
        db.Remove(entity); await db.SaveChangesAsync(ct); return NoContent();
    }
}
