using Auction.API.Domain.Enums;

namespace Auction.API.Common.DTOs;

public class BidDto
{
	public string Id { get; set; }
	public string BidderUsername { get; set; }
	public decimal Amount { get; set; }
	public DateTime TimeStamp { get; set; }
}