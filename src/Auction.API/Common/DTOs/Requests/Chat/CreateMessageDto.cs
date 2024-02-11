namespace Auction.API.Common.DTOs.Requests.Chat;

public class CreateMessageDto
{
    public string SenderId { get; set; }
    
    public string Content { get; set; }
    
    public string ParentMessageId { get; set; }
}