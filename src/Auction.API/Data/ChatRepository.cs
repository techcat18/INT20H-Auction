using System.Linq.Expressions;
using Auction.API.Common.Constants;
using Auction.API.Data.Interfaces;
using Auction.API.Domain.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace Auction.API.Data;

public class ChatRepository : GenericRepository<Chat>, IChatRepository
{
    protected override string ContainerName => Constants.CosmosDb.ContainerNames.Chats;
    
    public ChatRepository(CosmosClient cosmosClient) : base(cosmosClient)
    {
    }
    public override async Task<IEnumerable<Chat>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var feedIterator = Container
            .GetItemLinqQueryable<Chat>()
            .Where(x => x.Type == Constants.DbTypes.Message)
            .ToFeedIterator();

        var result = new List<Chat>();

        while (feedIterator.HasMoreResults)
        {
            var response = await feedIterator.ReadNextAsync(cancellationToken);
            result.AddRange(response);
        }

        return result;
    }

    public override async Task CreateAsync(
        Chat entity,
        string partitionKey,
        CancellationToken cancellationToken = default)
    {
        SetId(entity);
        SetAuditProperties(entity);

        entity.ChatId = entity.Id;

        await Container.CreateItemAsync(
            entity,
            GetPartitionKey(partitionKey, entity.Id),
            cancellationToken: cancellationToken);
    }

}