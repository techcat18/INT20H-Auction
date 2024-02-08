using Auction.API.Common.Constants;
using Auction.API.Data.Interfaces;
using Auction.API.Domain.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace Auction.API.Data;

public class LotRepository: GenericRepository<Lot>, ILotRepository
{
    protected override string ContainerName => Constants.CosmosDb.ContainerNames.Auction;
    
    public LotRepository(CosmosClient cosmosClient) : base(cosmosClient)
    {
    }

    public override async Task<IEnumerable<Lot>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var feedIterator = Container
            .GetItemLinqQueryable<Lot>()
            .Where(x => x.Type == Constants.DbTypes.Lot)
            .ToFeedIterator();

        var result = new List<Lot>();

        while (feedIterator.HasMoreResults)
        {
            var response = await feedIterator.ReadNextAsync(cancellationToken);
            result.AddRange(response);
        }

        return result;
    }
    
    public override async Task CreateAsync(
        Lot entity, 
        string partitionKey, 
        CancellationToken cancellationToken = default)
    {
        SetId(entity);
        SetAuditProperties(entity);

        entity.LotId = entity.Id;

        await Container.CreateItemAsync(
            entity,
            GetPartitionKey(partitionKey, entity.Id),
            cancellationToken: cancellationToken);
    }
}