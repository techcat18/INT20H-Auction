using Auction.API.Common.Constants;
using Auction.API.Data.Interfaces;
using Auction.API.Domain.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace Auction.API.Data;

public class BidRepository: GenericRepository<Bid>, IBidRepository
{
    protected override string ContainerName => Constants.CosmosDb.ContainerNames.Auction;
    
    public BidRepository(CosmosClient cosmosClient) : base(cosmosClient)
    {
    }

    public override async Task<IEnumerable<Bid>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var feedIterator = Container
            .GetItemLinqQueryable<Bid>()
            .Where(x => x.Type == Constants.DbTypes.Bid)
            .ToFeedIterator();

        var result = new List<Bid>();

        while (feedIterator.HasMoreResults)
        {
            var response = await feedIterator.ReadNextAsync(cancellationToken);
            result.AddRange(response);
        }

        return result;
    }
}