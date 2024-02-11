using Newtonsoft.Json;

namespace Auction.API.Domain.Entities;

public class User : BaseEntity
{
    [JsonRequired]
    [JsonProperty("userName")]
    public string UserName { get; set; }

    [JsonProperty("firstName")]
    public string FirstName{ get; set; }

    [JsonProperty("lastName")]
    public string LastName{ get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonRequired]
    [JsonProperty("passwordHash")]
    public string PasswordHash { get; set; }

    [JsonProperty("phoneNumber")]
    public string PhoneNumber { get; set; }
}