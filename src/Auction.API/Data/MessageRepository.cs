using Auction.API.Common.Constants;
using Auction.API.Domain.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace Auction.API.Data.Interfaces;

public class MessageRepository: GenericRepository<Message>, IMessageRepository
{
    public MessageRepository(CosmosClient cosmosClient) : base(cosmosClient)
    {
    }

    protected override string ContainerName => Constants.CosmosDb.ContainerNames.Chats;
    
    public async Task<IEnumerable<Message>> GetByChatIdAsync(
        string chatId, 
        CancellationToken cancellationToken = default)
    {
        var feedIterator = Container
            .GetItemLinqQueryable<Message>()
            .Where(x => x.ChatId == chatId && x.Type == Constants.DbTypes.Message)
            .ToFeedIterator();

        var result = new List<Message>();

        while (feedIterator.HasMoreResults)
        {
            var response = await feedIterator.ReadNextAsync(cancellationToken);
            result.AddRange(response);
        }

        return result;
    }
    
    
    
    
    
}