using Librarium.Data.Entities;

namespace Librarium.Data.Repositories.Interfaces;

public interface IMemberRepository
{
    Task<Member> CreateAsync(Member member);
    Task<List<Member>> GetAllAsync();
    Task<Member?> GetByIdAsync(int id);
}