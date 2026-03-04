using Librarium.Data.Entities;

namespace Librarium.Data.Interfaces;

public interface ILoanRepository
{
    Task<Loan> AddAsync(Loan loan);
    Task<List<Loan>> GetByMemberIdAsync(int memberId);
}