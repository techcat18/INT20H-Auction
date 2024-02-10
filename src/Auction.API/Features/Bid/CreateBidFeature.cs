using Auction.API.Common.Constants;
using Auction.API.Common.DTOs;
using Auction.API.Data.Interfaces;
using Auction.API.Features.Bid.Commands;
using AutoMapper;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auction.API.Features.Bid;

public static class CreateLotFeature
{
    internal sealed class Handler : IRequestHandler<CreateBidCommand, BidDto>
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

        public async Task<BidDto> Handle(CreateBidCommand command, CancellationToken cancellationToken)
        {
            var bid = _mapper.Map<Domain.Entities.Bid>(command);

            await _bidRepository.CreateAsync(bid, bid.Id, cancellationToken);

            return _mapper.Map<BidDto>(bid);
        }
    }

    public class CreateLotEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost(Constants.ApiEndpoints.Bid.Create,
                async ([FromBody] CreateBidCommand createBidCommand, ISender sender) =>
                {
                    var Bid = await sender.Send(createBidCommand);
                    return Results.Ok(bid);
                });
        }
    }
}