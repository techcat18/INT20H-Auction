using Auction.API.Common.DTOs;
using Auction.API.Domain.Entities;
using AutoMapper;

namespace Auction.API.Common.MappingProfiles;

public class ChatProfile : Profile
{
    public ChatProfile()
    {
        CreateMap<Chat, ChatDto>();
    }
}