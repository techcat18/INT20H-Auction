using Auction.API.Common.Constants;
using Auction.API.Domain.Enums;
using Newtonsoft.Json;

namespace Auction.API.Domain.Entities;

public class User : BaseEntity
{

    [JsonRequired]
    [JsonProperty("username")]
    public string UserName { get; set; }

    [JsonProperty("firstname")]
    public string FirstName{ get; set; }

    [JsonProperty("lastname")]
    public string LastName{ get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonRequired]
    [JsonProperty("passwordhash")]
    public string PasswordHash { get; set; }

    [JsonProperty("phonenumber")]
    public string PhoneNumber { get; set; }
}