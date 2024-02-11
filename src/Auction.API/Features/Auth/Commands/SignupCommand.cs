using Auction.API.Common.DTOs.Responses.Auth;
using MediatR;

namespace Auction.API.Features.Auth.Commands;

public record SignupCommand: IRequest<UserDto>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}