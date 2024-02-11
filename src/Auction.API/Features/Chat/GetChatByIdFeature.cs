using Auction.API.Common.Constants;
using Auction.API.Common.DTOs;
using Auction.API.Data.Interfaces;
using Auction.API.Domain.Entities;
using Auction.API.Features.Lot.Queries;
using Auction.API.Features.Message.Queries;
using AutoMapper;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auction.API.Features.Message;

public class GetChatByIdFeature 
{
    internal sealed class Handler : IRequestHandler<GetChatByIdQuery, ChatDto>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMapper _mapper;

        public Handler(
            IChatRepository chatRepository, 
            IMapper mapper)
        {
            _chatRepository = chatRepository;
            _mapper = mapper;
        }

        public async Task<ChatDto> Handle(GetChatByIdQuery query, CancellationToken cancellationToken)
        {
            var chat = await _chatRepository.GetByIdAsync(query.ChatId,query.ChatId, cancellationToken);
            return _mapper.Map<ChatDto>(chat);
        }
    }
    
    public class GetLotsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(Constants.ApiEndpoints.Chat.GetById, 
                async ([FromRoute] string id, ISender sender) =>
                {
                    var chat = await sender.Send(new GetChatByIdQuery{ChatId = id});
                    return Results.Ok(chat);
                });
        }
    }
}