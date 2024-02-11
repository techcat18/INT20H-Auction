using Auction.API.Common.DTOs;
using Auction.API.Common.DTOs.Responses;
using Auction.API.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auction.API.Features.Lot.Commands;

public class UpsertLotCommand : IRequest<LotDto>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public decimal StartPrice { get; set; }
    public LotStatus Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Image { get; set; }
    
}