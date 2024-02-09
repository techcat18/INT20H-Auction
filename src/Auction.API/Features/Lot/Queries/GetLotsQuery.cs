﻿using Auction.API.Common.DTOs;
using MediatR;

namespace Auction.API.Features.Lot.Queries;

public record GetLotsQuery: IRequest<IEnumerable<LotDto>>
{
}