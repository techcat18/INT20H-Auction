using Auction.API.Common.DTOs;
using MediatR;

namespace Auction.API.Features.Bid.Queries;

public record GetBidByIdQuery : IRequest<BidDto>
{
    public string Id { get; set; }
}