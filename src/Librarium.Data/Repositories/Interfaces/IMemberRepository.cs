using Librarium.Data.Entities;

namespace Librarium.Data.Repositories.Interfaces;

public interface IMemberRepository
{
    Task<List<Member>> GetAllAsync();
}