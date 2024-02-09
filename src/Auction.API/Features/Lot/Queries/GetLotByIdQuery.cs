using Auction.API.Common.DTOs;
using MediatR;

namespace Auction.API.Features.Lot.Queries;

public record GetLotByIdQuery : IRequest<LotDto>
{
    public string Id { get; set; }
}