using Auction.API.Common.Constants;
using Auction.API.Data.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using User = Auction.API.Domain.Entities.User;

namespace Auction.API.Data;

public class UserRepository: GenericRepository<User>, IUserRepository
{
    protected override string ContainerName => Constants.CosmosDb.ContainerNames.Users;
    
    public UserRepository(CosmosClient cosmosClient) : base(cosmosClient)
    {
    }

    public async Task<User> GetByEmailAsync(
        string email, 
        CancellationToken cancellationToken = default)
    {
        var feedIterator = Container
            .GetItemLinqQueryable<User>()
            .Where(x => x.Email == email)
            .ToFeedIterator();

        var result = new List<User>();

        while (feedIterator.HasMoreResults)
        {
            var response = await feedIterator.ReadNextAsync(cancellationToken);
            result.AddRange(response);
        }

        return result.FirstOrDefault();
    }
}