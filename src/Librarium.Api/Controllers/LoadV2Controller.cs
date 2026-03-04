using Librarium.Api.Dtos;
using Librarium.Api.Services;
using Librarium.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Librarium.Api.Controllers;

[ApiController]
[Route("api/v2/loans")]
public class LoansV2Controller : ControllerBase
{
    private readonly ILoanService _loanService;

    public LoansV2Controller(ILoanService loanService)
    {
        _loanService = loanService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateLoan(CreateLoanRequest request)
    {
        var result = await _loanService.CreateLoanV2Async(request);
        return Created("", result);
    }

    [HttpGet("{memberId}")]
    public async Task<IActionResult> GetLoans(int memberId)
    {
        var result = await _loanService.GetLoansV2Async(memberId);
        return Ok(result);
    }
}