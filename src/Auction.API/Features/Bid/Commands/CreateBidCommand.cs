using Auction.API.Common.DTOs;
using Auction.API.Common.DTOs.Responses;
using MediatR;

namespace Auction.API.Features.Bid.Commands;

public record CreateBidCommand : IRequest<BidDto>
{
    public string UserId { get; set; }
    public decimal Amount { get; set; }
    public string LotId { get; set; }

}