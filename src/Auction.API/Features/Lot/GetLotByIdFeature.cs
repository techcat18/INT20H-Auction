using Auction.API.Common.Constants;
using Auction.API.Common.DTOs;
using Auction.API.Data.Interfaces;
using Auction.API.Features.Lot.Queries;
using AutoMapper;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auction.API.Features.Lot;

public class GetLotByIdFeature
{
    internal sealed class Handler : IRequestHandler<GetLotByIdQuery, LotDto>
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

        public async Task<LotDto> Handle(GetLotByIdQuery query, CancellationToken cancellationToken)
        {
            var lot = await _lotRepository.GetByIdAsync(query.Id,query.Id, cancellationToken);
            return _mapper.Map<LotDto>(lot);
        }
    }
    
    public class GetLotsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(Constants.ApiEndpoints.Lot.GetById, 
                async ([FromRoute] string id, ISender sender) =>
                {
                    var lot = await sender.Send(new GetLotByIdQuery{Id = id});
                    return Results.Ok(lot);
                });
        }
    }
}