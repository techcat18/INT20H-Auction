using Newtonsoft.Json;

namespace Auction.API.Common.DTOs;

public class BidDto
{
    [JsonProperty("id")]
    public string Id { get; set; }
    
    [JsonProperty("userId")]
    public string UserId { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("timeStamp")]
    public DateTime TimeStamp { get; set; }
}