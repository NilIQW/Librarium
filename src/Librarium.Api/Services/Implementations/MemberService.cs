using Librarium.Api.Dtos;
using Librarium.Api.Services.Interfaces;
using Librarium.Data.Entities;
using Librarium.Data.Repositories.Interfaces;

namespace Librarium.Api.Services.Implementations;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _repository;

    public MemberService(IMemberRepository repository)
    {
        _repository = repository;
    }

    public async Task<MemberDto> CreateAsync(CreateMemberRequest request)
    {
        var member = new Member
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber
        };

        var created = await _repository.CreateAsync(member);

        return new MemberDto(
            created.MemberId,
            created.FirstName,
            created.LastName,
            created.Email,
            created.PhoneNumber
        );
    }

    public async Task<List<MemberDto>> GetAllAsync()
    {
        var members = await _repository.GetAllAsync();

        return members.Select(m =>
            new MemberDto(
                m.MemberId,
                m.FirstName,
                m.LastName,
                m.Email,
                m.PhoneNumber
            )).ToList();
    }

    public async Task<MemberDto?> GetByIdAsync(int id)
    {
        var member = await _repository.GetByIdAsync(id);
        if (member == null) return null;

        return new MemberDto(
            member.MemberId,
            member.FirstName,
            member.LastName,
            member.Email,
            member.PhoneNumber
        );
    }
}