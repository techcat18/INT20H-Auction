using Newtonsoft.Json;

namespace Auction.API.Domain.Entities;

public class Message : BaseEntity
{
    [JsonProperty("chatId")]
    [JsonRequired]
    public string ChatId { get; set; }
    
    [JsonProperty("senderId")]
    [JsonRequired]
    public string SenderId { get; set; }
    
    [JsonProperty("content")]
    [JsonRequired]
    public string Content { get; set; }
    
    [JsonProperty("parentMessageId")]
    public string ParentMessageId { get; set; }
}