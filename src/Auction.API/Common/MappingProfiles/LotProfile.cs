﻿using Auction.API.Common.DTOs;
using Auction.API.Common.DTOs.Responses;
using Auction.API.Domain.Entities;
using Auction.API.Features.Lot.Commands;
using AutoMapper;

namespace Auction.API.Common.MappingProfiles;

public class LotProfile: Profile
{
    public LotProfile()
    {
        CreateMap<CreateLotCommand, Lot>();
        CreateMap<UpsertLotCommand, Lot>();
        CreateMap<Lot, LotDto>();
    }
}