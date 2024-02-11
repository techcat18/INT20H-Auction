using Newtonsoft.Json;

namespace Auction.API.Domain.Entities;

public class Chat : BaseEntity
{
    [JsonProperty("lotId")]
    [JsonRequired]
    public string LotId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
}