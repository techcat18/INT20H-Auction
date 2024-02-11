using Auction.API.Common.Constants;
using Auction.API.Common.DTOs.Responses;
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
        private readonly IBidRepository _bidRepository;
        private readonly IMapper _mapper;

        public Handler(
            ILotRepository lotRepository, 
            IBidRepository bidRepository,
            IMapper mapper)
        {
            _lotRepository = lotRepository;
            _bidRepository = bidRepository;
            _mapper = mapper;
        }

        public async Task<LotDto> Handle(GetLotByIdQuery query, CancellationToken cancellationToken)
        {
            var lot = await _lotRepository.GetByIdAsync(query.Id,query.Id, cancellationToken);
            if (lot is null)
            {
                throw new Exception("Lot was not found");
            }

            var lotDto = _mapper.Map<LotDto>(lot);
            var bids = await _bidRepository.GetByLotIdAsync(lot.Id, cancellationToken);
            lotDto.Bids = _mapper.Map<ICollection<BidDto>>(bids);

            return lotDto;
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