using Librarium.Data.Entities;

namespace Librarium.Data.Repositories.Interfaces;

public interface ILoanRepository
{
    Task<Loan> CreateAsync(Loan loan);
    Task<List<Loan>> GetByMemberIdAsync(int memberId);
}