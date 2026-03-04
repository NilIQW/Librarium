using Librarium.Api.Dtos;
using Librarium.Api.Services;
using Librarium.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Librarium.Api.Controllers;

[ApiController]
[Route("api/members")]
public class MembersController : ControllerBase
{
    private readonly IMemberService _service;

    public MembersController(IMemberService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMemberRequest request)
    {
        var result = await _service.CreateAsync(request);
        return Created("", result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var member = await _service.GetByIdAsync(id);
        return member == null ? NotFound() : Ok(member);
    }
}