using Auction.API.Common.Constants;
using Newtonsoft.Json;

namespace Auction.API.Domain.Entities;

public class Bid: BaseEntity
{
    [JsonProperty("lotId")]
    public string LotId { get; set; }
    
    [JsonProperty("type")] 
    public string Type { get; set; } = Constants.DbTypes.Bid;

    [JsonProperty("userId")]
    public string UserId { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("timeStamp")]
    public DateTime TimeStamp { get; set; }
}