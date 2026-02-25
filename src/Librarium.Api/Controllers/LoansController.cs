using Librarium.Data;
using Librarium.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Librarium.Api.Controllers;

[ApiController]
[Route("api/loans")]
public class LoansController : ControllerBase
{
    private readonly LibrariumDbContext _context;

    public LoansController(LibrariumDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateLoan(CreateLoanRequest request)
    {
        var loan = new Loan
        {
            BookId = request.BookId,
            MemberId = request.MemberId,
            LoanDate = DateTime.UtcNow
        };

        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLoans), new { memberId = request.MemberId }, loan);
    }

    [HttpGet("{memberId}")]
    public async Task<IActionResult> GetLoans(int memberId)
    {
        var loans = await _context.Loans
            .Where(l => l.MemberId == memberId)
            .ToListAsync();

        return Ok(loans);
    }
}

public record CreateLoanRequest(int BookId, int MemberId);