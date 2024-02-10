using Auction.API.Common.Constants;
using Auction.API.Common.DTOs;
using Auction.API.Data.Interfaces;
using Auction.API.Features.Bid.Commands;
using Auction.API.Features.Bid.Queries;
using AutoMapper;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auction.API.Features.Bid;

public static class GetBidsFeature
{
    internal sealed class Handler : IRequestHandler<GetBidsQuery, IEnumerable<BidDto>>
    {
        private readonly IBidRepository _BidRepository;
        private readonly IMapper _mapper;

        public Handler(
            IBidRepository bidRepository,
            IMapper mapper)
        {
            _bidRepository = bidRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BidDto>> Handle(GetBidsQuery query, CancellationToken cancellationToken)
        {
            var lots = await _bidRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<BidDto>>(bids);
        }
    }

    public class GetBidsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(Constants.ApiEndpoints.Bid.GetAll,
                async (ISender sender) =>
                {
                    var bids = await sender.Send(new GetBidsQuery());
                    return Results.Ok(bids);
                });
        }
    }
}