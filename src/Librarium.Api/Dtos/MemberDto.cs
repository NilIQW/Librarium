namespace Librarium.Api.Dtos;

public record CreateMemberRequest(string FirstName, string LastName, string Email, string PhoneNumber);

public record MemberDto(int MemberId, string FirstName, string LastName, string Email, string PhoneNumber);