using Librarium.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Librarium.Api.Controllers;

[ApiController]
[Route("api/members")]
public class MembersController : ControllerBase
{
    private readonly LibrariumDbContext _context;

    public MembersController(LibrariumDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetMembers()
    {
        var members = await _context.Members.ToListAsync();
        return Ok(members);
    }
}