using Auction.API.Domain.Enums;

namespace Auction.API.Common.DTOs;

public class LotDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public decimal StartPrice { get; set; }
    public decimal? CurrentPrice { get; set; }
    public LotStatus Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Image { get; set; }
}