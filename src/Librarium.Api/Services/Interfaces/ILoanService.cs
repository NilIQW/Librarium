
using Librarium.Api.Dtos;

namespace Librarium.Api.Services.Interfaces;

public interface ILoanService
{
    Task<LoanV1Dto> CreateLoanV1Async(CreateLoanRequest request);
    Task<LoanV2Dto> CreateLoanV2Async(CreateLoanRequest request);

    Task<List<LoanV1Dto>> GetLoansV1Async(int memberId);
    Task<List<LoanV2Dto>> GetLoansV2Async(int memberId);
}