using Newtonsoft.Json;

namespace Auction.API.Domain;

public class BaseEntity
{
    [System.Text.Json.Serialization.JsonRequired]
    [JsonProperty("id")]
    public string Id { get; set; }
    
    [JsonProperty("createdAt")]
    public DateTime? CreatedAt { get; set; }
    
    [JsonProperty("modifiedAt")]
    public DateTime? ModifiedAt { get; set; }
}