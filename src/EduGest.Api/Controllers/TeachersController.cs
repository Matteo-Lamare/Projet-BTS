using EduGest.Domain.Entities;
using EduGest.Infrastructure.Identity;
using EduGest.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduGest.Api.Controllers;

[ApiController, Route("api/teachers")]
public sealed class TeachersController(EduGestDbContext db, UserManager<ApplicationUser> users) : ControllerBase
{
    public sealed record CreateRequest(string UserName,string Email,string Password,string FirstName,string LastName);
    public sealed record UpdateRequest(string FirstName,string LastName);

    [HttpGet, Authorize(Policy="Permission:teachers.read")]
    public async Task<IActionResult> GetAll(CancellationToken ct)=>Ok(await db.Teachers.AsNoTracking().OrderBy(x=>x.LastName).ThenBy(x=>x.FirstName).ToListAsync(ct));
    [HttpGet("{id:guid}"), Authorize(Policy="Permission:teachers.read")]
    public async Task<IActionResult> Get(Guid id,CancellationToken ct)=>await db.Teachers.AsNoTracking().SingleOrDefaultAsync(x=>x.Id==id,ct) is { } x?Ok(x):NotFound();

    [HttpPost, Authorize(Policy="Permission:teachers.write")]
    public async Task<IActionResult> Create(CreateRequest r,CancellationToken ct)
    {
        var first=r.FirstName.Trim();var last=r.LastName.Trim();if(first.Length is <1 or >100||last.Length is <1 or >100)return BadRequest();
        if(await users.FindByNameAsync(r.UserName.Trim()) is not null||await users.FindByEmailAsync(r.Email.Trim()) is not null)return Conflict();
        await using var tx=await db.Database.BeginTransactionAsync(ct);
        var user=new ApplicationUser{Id=Guid.NewGuid(),UserName=r.UserName.Trim(),Email=r.Email.Trim(),EmailConfirmed=true};
        var created=await users.CreateAsync(user,r.Password);if(!created.Succeeded)return BadRequest(created.Errors);
        await users.AddToRoleAsync(user,"Teacher");
        var entity=new Teacher{Id=Guid.NewGuid(),UserId=user.Id,FirstName=first,LastName=last};db.Teachers.Add(entity);await db.SaveChangesAsync(ct);await tx.CommitAsync(ct);
        return CreatedAtAction(nameof(Get),new{id=entity.Id},entity);
    }

    [HttpPut("{id:guid}"), Authorize(Policy="Permission:teachers.write")]
    public async Task<IActionResult> Update(Guid id,UpdateRequest r,CancellationToken ct){var x=await db.Teachers.FindAsync([id],ct);if(x is null)return NotFound();var f=r.FirstName.Trim();var l=r.LastName.Trim();if(f.Length is <1 or >100||l.Length is <1 or >100)return BadRequest();x.FirstName=f;x.LastName=l;await db.SaveChangesAsync(ct);return NoContent();}

    [HttpDelete("{id:guid}"), Authorize(Policy="Permission:teachers.write")]
    public async Task<IActionResult> Delete(Guid id,CancellationToken ct){var x=await db.Teachers.FindAsync([id],ct);if(x is null)return NotFound();if(await db.TeachingAssignments.AnyAsync(a=>a.TeacherId==id,ct))return Conflict("Ce professeur possède des affectations.");var user=await users.FindByIdAsync(x.UserId.ToString());db.Teachers.Remove(x);await db.SaveChangesAsync(ct);if(user is not null)await users.DeleteAsync(user);return NoContent();}
}
