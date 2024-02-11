namespace Auction.API.Common.DTOs.Requests.Bid;

public class CreateBidDto
{
    public string UserId { get; set; }
    
    public decimal Amount { get; set; }
}