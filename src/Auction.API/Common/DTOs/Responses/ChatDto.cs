using Azure.Core;

namespace Auction.API.Common.DTOs;

public class ChatDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string LotId { get; set; }
}