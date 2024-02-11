using Auction.API.Domain.Entities;

namespace Auction.API.Data.Interfaces;

public interface IMessageRepository : IGenericRepository<Message>
{
    Task<IEnumerable<Message>> GetByChatIdAsync(string chatId, CancellationToken cancellationToken = default);
}