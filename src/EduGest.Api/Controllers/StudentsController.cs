using EduGest.Domain.Entities;
using EduGest.Infrastructure.Identity;
using EduGest.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduGest.Api.Controllers;

[ApiController, Route("api/students")]
public sealed class StudentsController(EduGestDbContext db, UserManager<ApplicationUser> users) : ControllerBase
{
    public sealed record CreateRequest(string UserName, string Email, string Password, string FirstName, string LastName, DateTime? BirthDate);
    public sealed record UpdateRequest(string FirstName, string LastName, DateTime? BirthDate);

    [HttpGet, Authorize(Policy="Permission:students.read")]
    public async Task<IActionResult> GetAll(CancellationToken ct) => Ok(await db.Students.AsNoTracking().OrderBy(x=>x.LastName).ThenBy(x=>x.FirstName).ToListAsync(ct));

    [HttpGet("{id:guid}"), Authorize(Policy="Permission:students.read")]
    public async Task<IActionResult> Get(Guid id, CancellationToken ct) => await db.Students.AsNoTracking().SingleOrDefaultAsync(x=>x.Id==id,ct) is { } x ? Ok(x) : NotFound();

    [HttpPost, Authorize(Policy="Permission:students.write")]
    public async Task<IActionResult> Create(CreateRequest r, CancellationToken ct)
    {
        var first=r.FirstName.Trim(); var last=r.LastName.Trim();
        if(first.Length is <1 or >100 || last.Length is <1 or >100 || string.IsNullOrWhiteSpace(r.UserName) || string.IsNullOrWhiteSpace(r.Email)) return BadRequest();
        if(await users.FindByNameAsync(r.UserName.Trim()) is not null || await users.FindByEmailAsync(r.Email.Trim()) is not null) return Conflict("Le compte existe déjà.");
        await using var tx=await db.Database.BeginTransactionAsync(ct);
        var user=new ApplicationUser{Id=Guid.NewGuid(),UserName=r.UserName.Trim(),Email=r.Email.Trim(),EmailConfirmed=true};
        var created=await users.CreateAsync(user,r.Password);
        if(!created.Succeeded) return BadRequest(created.Errors);
        await users.AddToRoleAsync(user,"Student");
        var entity=new Student{Id=Guid.NewGuid(),UserId=user.Id,FirstName=first,LastName=last,BirthDate=r.BirthDate};
        db.Students.Add(entity); await db.SaveChangesAsync(ct); await tx.CommitAsync(ct);
        return CreatedAtAction(nameof(Get),new{id=entity.Id},entity);
    }

    [HttpPut("{id:guid}"), Authorize(Policy="Permission:students.write")]
    public async Task<IActionResult> Update(Guid id, UpdateRequest r, CancellationToken ct)
    {
        var x=await db.Students.FindAsync([id],ct); if(x is null)return NotFound();
        var first=r.FirstName.Trim(); var last=r.LastName.Trim(); if(first.Length is <1 or >100 || last.Length is <1 or >100)return BadRequest();
        x.FirstName=first;x.LastName=last;x.BirthDate=r.BirthDate;await db.SaveChangesAsync(ct);return NoContent();
    }

    [HttpDelete("{id:guid}"), Authorize(Policy="Permission:students.write")]
    public async Task<IActionResult> Delete(Guid id,CancellationToken ct)
    {
        var x=await db.Students.FindAsync([id],ct);if(x is null)return NotFound();
        if(await db.Enrollments.AnyAsync(e=>e.StudentId==id,ct)||await db.Grades.AnyAsync(g=>g.StudentId==id,ct)||await db.AttendanceEvents.AnyAsync(a=>a.StudentId==id,ct))return Conflict("Cet élève possède des données scolaires.");
        var user=await users.FindByIdAsync(x.UserId.ToString());db.Students.Remove(x);await db.SaveChangesAsync(ct);if(user is not null)await users.DeleteAsync(user);return NoContent();
    }
}
