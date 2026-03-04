using Librarium.Api.Dtos;
using Librarium.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Librarium.Api.Controllers;

[ApiController]
[Route("api/loans")]
public class LoansController : ControllerBase
{
    private readonly ILoanService _loanService;

    public LoansController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateLoan(CreateLoanRequest request)
    {
        var result = await _loanService.CreateLoanV1Async(request);
        return Created("", result);
    }

    [HttpGet("{memberId}")]
    public async Task<IActionResult> GetLoans(int memberId)
    {
        var result = await _loanService.GetLoansV1Async(memberId);
        return Ok(result);
    }
}