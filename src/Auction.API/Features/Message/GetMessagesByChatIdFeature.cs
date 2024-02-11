using Auction.API.Common.Constants;
using Auction.API.Common.DTOs;
using Auction.API.Data.Interfaces;
using Auction.API.Features.Bid.Queries;
using Auction.API.Features.Message.Queries;
using AutoMapper;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auction.API.Features.Message;

public class GetMessagesByChatIdFeature
{
    internal sealed class Handler : IRequestHandler<GetMessagesByChatIdQuery, IEnumerable<MessageDto>>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public Handler(
            IMessageRepository messageRepository,
            IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MessageDto>> Handle(GetMessagesByChatIdQuery query, CancellationToken cancellationToken)
        {
            var messages = await _messageRepository.GetByChatIdAsync(query.ChatId, cancellationToken);
            return _mapper.Map<IEnumerable<MessageDto>>(messages);
        }
    }

    public class GetMessagesByChatIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(Constants.ApiEndpoints.Message.GetByChatId,
                async ([FromRoute] string chatId, ISender sender) =>
                {
                    var messages = await sender
                        .Send(new GetMessagesByChatIdQuery{ChatId = chatId});
                    return Results.Ok(messages);
                });
        }
    }
}