using Auction.API.Common.DTOs;
using Auction.API.Domain.Entities;
using Auction.API.Features.Message.Commands;
using AutoMapper;

namespace Auction.API.Common.MappingProfiles;

public class MessageProfile : Profile
{
    public MessageProfile()
    {
        CreateMap<Message, MessageDto>();
        CreateMap<CreateMessageCommand, Message>();
    }
}