using Librarium.Api.Dtos;
using Librarium.Api.Services.Interfaces;
using Librarium.Data.Entities;
using Librarium.Data.Repositories.Interfaces;

namespace Librarium.Api.Services.Implementations;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;

    public LoanService(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    public async Task<LoanV1Dto> CreateLoanV1Async(CreateLoanRequest request)
    {
        var loan = new Loan
        {
            BookId = request.BookId,
            MemberId = request.MemberId,
            LoanDate = DateTime.UtcNow
        };

        var created = await _loanRepository.CreateAsync(loan);

        return new LoanV1Dto
        {
            LoanId = created.LoanId,
            BookTitle = created.Book?.Title ?? "",
            LoanDate = created.LoanDate,
            ReturnDate = created.ReturnDate
        };
    }

    public async Task<LoanV2Dto> CreateLoanV2Async(CreateLoanRequest request)
    {
        var loan = new Loan
        {
            BookId = request.BookId,
            MemberId = request.MemberId,
            LoanDate = DateTime.UtcNow,
            Status = LoanStatus.Active
        };

        var created = await _loanRepository.CreateAsync(loan);

        return new LoanV2Dto
        {
            LoanId = created.LoanId,
            BookTitle = created.Book?.Title ?? "",
            LoanDate = created.LoanDate,
            ReturnDate = created.ReturnDate,
            Status = created.Status.ToString()
        };
    }

    public async Task<List<LoanV1Dto>> GetLoansV1Async(int memberId)
    {
        var loans = await _loanRepository.GetByMemberIdAsync(memberId);

        return loans.Select(l => new LoanV1Dto
        {
            LoanId = l.LoanId,
            BookTitle = l.Book.Title,
            LoanDate = l.LoanDate,
            ReturnDate = l.ReturnDate
        }).ToList();
    }

    public async Task<List<LoanV2Dto>> GetLoansV2Async(int memberId)
    {
        var loans = await _loanRepository.GetByMemberIdAsync(memberId);

        return loans.Select(l => new LoanV2Dto
        {
            LoanId = l.LoanId,
            BookTitle = l.Book.Title,
            LoanDate = l.LoanDate,
            ReturnDate = l.ReturnDate,
            Status = l.Status.ToString()
        }).ToList();
    }
}