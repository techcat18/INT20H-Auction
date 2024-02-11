using Auction.API.Common.Constants;
using Auction.API.Common.DTOs;
using Auction.API.Data.Interfaces;
using Auction.API.Domain.Entities;
using Auction.API.Features.Lot.Commands;
using AutoMapper;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auction.API.Features.Lot;

public static class CreateLotFeature
{
    internal sealed class Handler : IRequestHandler<CreateLotCommand, LotDto>
    {
        private readonly ILotRepository _lotRepository;
        private readonly IChatRepository _chatRepository;
        private readonly IMapper _mapper;

        public Handler(
            ILotRepository lotRepository, 
            IMapper mapper,
            IChatRepository chatRepository)
        {
            _lotRepository = lotRepository;
            _mapper = mapper;
            _chatRepository = chatRepository;
        }

        public async Task<LotDto> Handle(CreateLotCommand command, CancellationToken cancellationToken)
        {
            var lot = _mapper.Map<Domain.Entities.Lot>(command);

            await _lotRepository.CreateAsync(lot, lot.Id, cancellationToken);
            var chat = new Chat { LotId = lot.LotId };
            await _chatRepository.CreateAsync(chat, chat.Id, cancellationToken);

            return _mapper.Map<LotDto>(lot);
        }
    }
    
    public class CreateLotEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost(Constants.ApiEndpoints.Lot.Create, 
                async ([FromBody]CreateLotCommand createLotCommand, ISender sender) =>
                {
                    var lot = await sender.Send(createLotCommand);
                    return Results.Ok(lot);
                });
        }
    }
}