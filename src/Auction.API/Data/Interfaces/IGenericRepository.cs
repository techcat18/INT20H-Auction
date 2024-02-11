using System.Linq.Expressions;
using Auction.API.Domain.Entities;
using Microsoft.Azure.Cosmos;

namespace Auction.API.Data.Interfaces;

public interface IGenericRepository<TEntity> where TEntity: BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetAllByConditionAsync(
        Expression<Func<TEntity, bool>> predicate = default,
        CancellationToken cancellationToken = default);

    Task<TEntity> GetByIdAsync(
        string id,
        string partitionKey,
        CancellationToken cancellationToken = default);

    Task CreateAsync(
        TEntity entity, 
        string partitionKey,
        CancellationToken cancellationToken = default);

    Task UpsertAsync(
        TEntity entity,
        string partitionKey = default,
        CancellationToken cancellationToken = default);

    Task PatchAsync<T>(
        string id,
        string partitionKey,
        List<PatchOperation> patchOperations,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        string id,
        string partitionKey,
        CancellationToken cancellationToken = default);
}