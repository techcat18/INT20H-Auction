using Auction.API.Common.DTOs;
using MediatR;

namespace Auction.API.Features.Message.Queries;

public class GetChatByIdQuery : IRequest<ChatDto>
{
    public string ChatId { get; set; }
}