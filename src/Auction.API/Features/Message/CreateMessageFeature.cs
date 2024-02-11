using Auction.API.Common.Constants;
using Auction.API.Common.DTOs;
using Auction.API.Common.DTOs.Requests.Chat;
using Auction.API.Data.Interfaces;
using Auction.API.Features.Message.Commands;
using AutoMapper;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auction.API.Features.Message;

public class CreateMessageFeature
{
    internal sealed class Handler : IRequestHandler<CreateMessageCommand, MessageDto>
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

        public async Task<MessageDto> Handle(CreateMessageCommand query, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.Message>(query);
            await _messageRepository.CreateAsync(entity, query.ChatId, cancellationToken);
            return _mapper.Map<MessageDto>(entity);
        }
    }

    public class CreateMessageEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost(Constants.ApiEndpoints.Message.CreateMessage,
                async ([FromRoute] string chatId, [FromBody] CreateMessageDto dto, ISender sender) =>
                {
                    var message = await sender
                        .Send(new CreateMessageCommand{ChatId = chatId,
                            Content = dto.Content, ParentMessageId =dto.ParentMessageId,
                            SenderId = dto.SenderId});
                    return Results.Ok(message);
                });
        }
    }
}