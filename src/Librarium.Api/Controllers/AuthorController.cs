using Librarium.Api.Dtos;
using Librarium.Api.Services;
using Librarium.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Librarium.Api.Controllers;

[ApiController]
[Route("api/authors")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _service;

    public AuthorsController(IAuthorService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAuthorRequest request)
    {
        var result = await _service.CreateAsync(request);
        return Created("", result);
    }
}