using Librarium.Data.Entities;
using Librarium.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Librarium.Data.Repositories.Implementations;

public class LoanRepository : ILoanRepository
{
    private readonly LibrariumDbContext _context;

    public LoanRepository(LibrariumDbContext context)
    {
        _context = context;
    }

    public async Task<Loan> CreateAsync(Loan loan)
    {
        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();
        return loan;
    }

    public async Task<List<Loan>> GetByMemberIdAsync(int memberId)
    {
        return await _context.Loans
            .Where(l => l.MemberId == memberId)
            .Include(l => l.Book)
            .ToListAsync();
    }
}