using Newtonsoft.Json;

namespace Auction.API.Common.DTOs;

public class BidDto
{
    public string Id { get; set; }
    
    public string UserId { get; set; }
    
    public decimal Amount { get; set; }
    
    public DateTime TimeStamp { get; set; }

    public string LotId { get; set; }
}