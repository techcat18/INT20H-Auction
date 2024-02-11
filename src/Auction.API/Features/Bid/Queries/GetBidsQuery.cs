using Auction.API.Common.DTOs;
using Auction.API.Common.DTOs.Responses;
using MediatR;

namespace Auction.API.Features.Bid.Queries;

public record GetBidsQuery : IRequest<IEnumerable<BidDto>>
{
}