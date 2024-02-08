using System.Linq.Expressions;
using System.Net;
using Auction.API.Common.Constants;
using Auction.API.Domain;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace Auction.API.Data;

public abstract class GenericRepository<TEntity> 
    : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    #region Protected properties

    protected abstract string ContainerName { get; }
    protected Container Container { get; set; }
    
    #endregion
    
    protected GenericRepository(CosmosClient cosmosClient)
    {
        Container = cosmosClient.GetContainer(Constants.CosmosDb.DatabaseName, ContainerName);
    }
    
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var feedIterator = Container
            .GetItemLinqQueryable<TEntity>()
            .ToFeedIterator();

        var result = new List<TEntity>();

        while (feedIterator.HasMoreResults)
        {
            var response = await feedIterator.ReadNextAsync(cancellationToken);
            result.AddRange(response);
        }

        return result;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllByConditionAsync(
        Expression<Func<TEntity, bool>> predicate = default,
        CancellationToken cancellationToken = default)
    {
        var query = Container
            .GetItemLinqQueryable<TEntity>()
            .AsQueryable();

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }
        
        var feedIterator = query.ToFeedIterator();
        var results = new List<TEntity>();

        while (feedIterator.HasMoreResults)
        {
            var response = await feedIterator.ReadNextAsync(cancellationToken);
            results.AddRange(response);
        }

        return results;
    }
    
    public virtual async Task<TEntity> GetByIdAsync(
        string id,
        string partitionKey,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await Container.ReadItemAsync<TEntity>(
                id,
                GetPartitionKey(partitionKey, id),
                cancellationToken: cancellationToken);

            return response.Resource;
        }
        catch (CosmosException e) when (e.StatusCode is HttpStatusCode.NotFound)
        {
            return default;
        }
    }

    public virtual async Task CreateAsync(
        TEntity entity, 
        string partitionKey, 
        CancellationToken cancellationToken = default)
    {
        SetId(entity);
        SetAuditProperties(entity);

        await Container.CreateItemAsync(
            entity,
            GetPartitionKey(partitionKey, entity.Id),
            cancellationToken: cancellationToken);
    }
    
    public virtual async Task UpsertAsync(
        TEntity entity, 
        string partitionKey = default, 
        CancellationToken cancellationToken = default)
    {
        SetAuditProperties(entity);

        await Container.UpsertItemAsync(
            entity,
            GetPartitionKey(partitionKey, entity.Id),
            cancellationToken: cancellationToken);
    }

    public virtual async Task PatchAsync<T>(
        string id,
        string partitionKey,
        List<PatchOperation> patchOperations,
        CancellationToken cancellationToken = default)
    {
        await Container.PatchItemAsync<T>(
            id,
            new PartitionKey(partitionKey),
            patchOperations, 
            cancellationToken: cancellationToken);
    }

    public virtual async Task DeleteAsync(
        string id, 
        string partitionKey, 
        CancellationToken cancellationToken = default)
    {
        await Container.DeleteItemAsync<TEntity>(
            id,
            GetPartitionKey(partitionKey, id),
            cancellationToken: cancellationToken);
    }

    #region Private methods

    private static void SetAuditProperties(TEntity entity)
    {
        entity.CreatedAt ??= DateTime.UtcNow;
        entity.ModifiedAt = DateTime.UtcNow;
    }

    #endregion

    #region Protected methods
    
    protected static void SetId(TEntity entity)
    {
        entity.Id ??= Guid.NewGuid().ToString();
    }
    
    protected static PartitionKey GetPartitionKey(string partitionKey, string id)
    {
        return string.IsNullOrWhiteSpace(partitionKey) 
            ? new PartitionKey(id) 
            : new PartitionKey(partitionKey);
    }
    
    #endregion
}