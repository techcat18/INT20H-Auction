using Auction.API.Common.DTOs;
using MediatR;

namespace Auction.API.Features.Message.Commands;

public class CreateMessageCommand : IRequest<MessageDto>
{
    public string ChatId { get; set; }
    
    public string SenderId { get; set; }
    
    public string Content { get; set; }
    
    public string ParentMessageId { get; set; }
}