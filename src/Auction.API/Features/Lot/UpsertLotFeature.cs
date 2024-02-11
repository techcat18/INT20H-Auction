using Auction.API.Common.Constants;
using Auction.API.Common.DTOs;
using Auction.API.Common.DTOs.Responses;
using Auction.API.Data.Interfaces;
using Auction.API.Features.Lot.Commands;
using AutoMapper;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auction.API.Features.Lot;

public class UpsertLotFeature
{
    internal sealed class Handler : IRequestHandler<UpsertLotCommand, LotDto>
    {
        private readonly ILotRepository _lotRepository;
        private readonly IMapper _mapper;

        public Handler(
            ILotRepository lotRepository, 
            IMapper mapper)
        {
            _lotRepository = lotRepository;
            _mapper = mapper;
        }

        public async Task<LotDto> Handle(UpsertLotCommand query, CancellationToken cancellationToken)
        {
            
            var entity = _mapper.Map<Domain.Entities.Lot>(query);
            entity.LotId = query.Id;
            await _lotRepository.UpsertAsync(entity, entity.Id, cancellationToken); 
            
            return _mapper.Map<LotDto>(entity);
        }
    }
    
    public class GetLotsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost(Constants.ApiEndpoints.Lot.Upsert, 
                async ([FromRoute] string id, [FromBody] UpsertLotCommand upsertLotCommand, ISender sender) =>
                {
                    upsertLotCommand.Id = id;
                    var lot = await sender.Send(upsertLotCommand);
                    return Results.Ok(lot);
                });
        }
    }
}