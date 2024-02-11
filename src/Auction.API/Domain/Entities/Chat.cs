using Auction.API.Common.Constants;
using Newtonsoft.Json;

namespace Auction.API.Domain.Entities;

public class Chat : BaseEntity
{
    
    [JsonProperty("chatId")]
    [JsonRequired]
    public string ChatId { get; set; }
    
    [JsonProperty("lotId")]
    [JsonRequired]
    public string LotId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("type")] 
    public string Type { get; set; } = Constants.DbTypes.Chat;
}