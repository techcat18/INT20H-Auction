using Auction.API.Common.Constants;
using Auction.API.Common.DTOs;
using Auction.API.Common.DTOs.Responses;
using Auction.API.Data.Interfaces;
using Auction.API.Features.Lot.Queries;
using AutoMapper;
using Carter;
using MediatR;

namespace Auction.API.Features.Lot;

public static class GetLotsFeature
{
    internal sealed class Handler : IRequestHandler<GetLotsQuery, IEnumerable<LotDto>>
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

        public async Task<IEnumerable<LotDto>> Handle(GetLotsQuery query, CancellationToken cancellationToken)
        {
            var lots = await _lotRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<LotDto>>(lots);
        }
    }
    
    public class GetLotsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(Constants.ApiEndpoints.Lot.GetAll, 
                async (ISender sender) =>
                {
                    var lots = await sender.Send(new GetLotsQuery());
                    return Results.Ok(lots);
                });
        }
    }
}