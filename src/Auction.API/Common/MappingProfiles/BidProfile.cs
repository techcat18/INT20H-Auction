using Auction.API.Common.DTOs;
using Auction.API.Domain.Entities;
using Auction.API.Features.Bid.Commands;
using AutoMapper;

namespace Auction.API.Common.MappingProfiles;

public class BidProfile: Profile
{
    public BidProfile()
    {
        CreateMap<CreateBidCommand, Bid>();
        CreateMap<Bid, BidDto>();
    }
}