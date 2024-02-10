using Auction.API.Common.DTOs;
using MediatR;

namespace Auction.API.Features.Bid.Queries;

public record GetBidsQuery : IRequest<IEnumerable<BidDto>>
{
}