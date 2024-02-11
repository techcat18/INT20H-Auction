using Auction.API.Domain.Entities;

namespace Auction.API.Data.Interfaces;

public interface IUserRepository: IGenericRepository<User>
{
    Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}