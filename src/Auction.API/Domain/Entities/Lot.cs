using Auction.API.Common.Constants;
using Auction.API.Domain.Enums;
using Newtonsoft.Json;

namespace Auction.API.Domain.Entities;

public class Lot: BaseEntity
{
    [JsonProperty("lotId")]
    public string LotId { get; set; }

    [JsonProperty("type")] 
    public string Type { get; set; } = Constants.DbTypes.Lot;
    
    [JsonRequired]
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("category")]
    public string Category { get; set; }

    [JsonRequired]
    [JsonProperty("startPrice")]
    public decimal StartPrice { get; set; }

    [JsonProperty("currentPrice")]
    public decimal? CurrentPrice { get; set; }

    [JsonProperty("status")] 
    public LotStatus Status { get; set; } = LotStatus.NotStarted;

    [JsonProperty("startDate")]
    public DateTime? StartDate { get; set; }

    [JsonProperty("endDate")]
    public DateTime? EndDate { get; set; }

    [JsonProperty("image")]
    public string Image { get; set; }
}