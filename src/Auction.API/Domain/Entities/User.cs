using Newtonsoft.Json;

namespace Auction.API.Domain.Entities;

public class User : BaseEntity
{
    [JsonRequired]
    [JsonProperty("userName")]
    public string UserName { get; set; }

    [JsonRequired]
    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonRequired]
    [JsonProperty("passwordHash")]
    public string PasswordHash { get; set; }
}