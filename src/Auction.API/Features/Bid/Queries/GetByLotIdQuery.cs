using Auction.API.Common.DTOs;
using MediatR;

namespace Auction.API.Features.Bid.Queries;

public record GetBidsByLotIdQuery: IRequest<IEnumerable<BidDto>>
{
    public string LotId { get; set; }
}