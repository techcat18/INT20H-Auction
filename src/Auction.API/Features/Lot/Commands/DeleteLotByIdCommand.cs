using Auction.API.Common.DTOs;
using Auction.API.Common.DTOs.Responses;
using MediatR;

namespace Auction.API.Features.Lot.Commands;

public class DeleteLotByIdCommand : IRequest<LotDto>
{
    public string Id { get; set; }
}