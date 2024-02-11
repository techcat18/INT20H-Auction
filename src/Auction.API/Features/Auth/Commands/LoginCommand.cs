using Auction.API.Common.DTOs.Responses.Auth;
using MediatR;

namespace Auction.API.Features.Auth.Commands;

public record LoginCommand: IRequest<AuthResponseDto>
{
    public string Email { get; set; }
    public string Password { get; set; }
}