using Auction.API.Domain.Entities;

namespace Auction.API.Data.Interfaces;

public interface IBidRepository: IGenericRepository<Bid>
{
    Task<IEnumerable<Bid>> GetByLotIdAsync(string lotId, CancellationToken cancellationToken = default);
}