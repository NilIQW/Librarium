using Librarium.Api.Dtos;

namespace Librarium.Api.Services.Interfaces;

public interface IMemberService
{
    Task<MemberDto> CreateAsync(CreateMemberRequest request);
    Task<List<MemberDto>> GetAllAsync();
    Task<MemberDto?> GetByIdAsync(int id);
}