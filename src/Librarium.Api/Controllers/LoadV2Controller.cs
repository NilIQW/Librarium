using Librarium.Data;
using Librarium.Data.Dtos;
using Librarium.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Librarium.Api.Controllers;

[ApiController]
[Route("api/v2/loans")]
public class LoansV2Controller : ControllerBase
{
    private readonly LibrariumDbContext _context;

    public LoansV2Controller(LibrariumDbContext context)
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
            LoanDate = DateTime.UtcNow,
            Status = LoanStatus.Active
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
            .Include(l => l.Book)
            .Select(l => new LoanV2Dto
            {
                LoanId = l.LoanId,
                BookTitle = l.Book.Title,
                LoanDate = l.LoanDate,
                ReturnDate = l.ReturnDate,
                Status = l.Status.ToString()
            })
            .ToListAsync();

        return Ok(loans);
    }
}