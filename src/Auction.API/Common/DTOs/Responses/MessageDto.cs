namespace Auction.API.Common.DTOs;

public class MessageDto
{
    public string Id { get; set; }
    
    public string ChatId { get; set; }
    
    public string SenderId { get; set; }
    
    public string Content { get; set; }
    
    public string ParentMessageId { get; set; }
}