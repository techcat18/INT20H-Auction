using Newtonsoft.Json;

namespace Auction.API.Domain.Entities;

public class BaseEntity
{
    [JsonRequired]
    [JsonProperty("id")]
    public string Id { get; set; }
    
    [JsonProperty("createdAt")]
    public DateTime? CreatedAt { get; set; }
    
    [JsonProperty("modifiedAt")]
    public DateTime? ModifiedAt { get; set; }
}