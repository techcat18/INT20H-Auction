using Auction.API.Common.DTOs;
using MediatR;

namespace Auction.API.Features.Message.Queries;

public class GetMessagesByChatIdQuery : IRequest<IEnumerable<MessageDto>>
{
    public string ChatId { get; set; }
}