using Auction.API.Common.DTOs;
using MediatR;

namespace Auction.API.Features.Lot.Queries;

public class GetLotsQuery: IRequest<IEnumerable<LotDto>>
{
}