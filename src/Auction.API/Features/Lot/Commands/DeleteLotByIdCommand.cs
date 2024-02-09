using Auction.API.Common.DTOs;
using MediatR;

namespace Auction.API.Features.Lot.Commands;

public class DeleteLotByIdCommand : IRequest<LotDto>
{
    public string Id { get; set; }
}