using Auction.API.Common.DTOs;
using Auction.API.Common.DTOs.Responses;
using MediatR;

namespace Auction.API.Features.Lot.Commands;

public record CreateLotCommand: IRequest<LotDto>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public decimal StartPrice { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Image { get; set; }
}