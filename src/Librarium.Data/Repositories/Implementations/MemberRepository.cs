using Librarium.Data.Entities;
using Librarium.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Librarium.Data.Repositories.Implementations;

public class MemberRepository : IMemberRepository
{
    private readonly LibrariumDbContext _context;

    public MemberRepository(LibrariumDbContext context)
    {
        _context = context;
    }

    public async Task<Member> CreateAsync(Member member)
    {
        _context.Members.Add(member);
        await _context.SaveChangesAsync();
        return member;
    }

    public async Task<List<Member>> GetAllAsync()
    {
        return await _context.Members.ToListAsync();
    }

    public async Task<Member?> GetByIdAsync(int id)
    {
        return await _context.Members.FindAsync(id);
    }
}