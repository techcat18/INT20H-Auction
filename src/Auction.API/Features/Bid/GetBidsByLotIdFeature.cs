using Auction.API.Common.Constants;
using Auction.API.Common.DTOs;
using Auction.API.Data.Interfaces;
using Auction.API.Features.Bid.Queries;
using AutoMapper;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auction.API.Features.Bid;

public class GetBidsByLotIdFeature
{
    internal sealed class Handler : IRequestHandler<GetBidsByLotIdQuery, IEnumerable<BidDto>>
    {
        private readonly IBidRepository _bidRepository;
        private readonly IMapper _mapper;

        public Handler(
            IBidRepository bidRepository,
            IMapper mapper)
        {
            _bidRepository = bidRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BidDto>> Handle(GetBidsByLotIdQuery query, CancellationToken cancellationToken)
        {
            var bids = await _bidRepository.GetByLotIdAsync(query.LotId, cancellationToken);
            return _mapper.Map<IEnumerable<BidDto>>(bids);
        }
    }

    public class GetBidsByLotIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(Constants.ApiEndpoints.Bid.GetByLotId,
                async ([FromRoute] string lotId, ISender sender) =>
                {
                    var bids = await sender.Send(new GetBidsByLotIdQuery{LotId = lotId});
                    return Results.Ok(bids);
                });
        }
    }
}