using Auction.API.Common.DTOs.Requests.Auth;
using Auction.API.Common.DTOs.Responses.Auth;
using Auction.API.Domain.Entities;
using Auction.API.Features.Auth.Commands;
using AutoMapper;

namespace Auction.API.Common.MappingProfiles;

public class AuthProfile: Profile
{
    public AuthProfile()
    {
        CreateMap<LoginDto, LoginCommand>();
        CreateMap<SignupDto, SignupCommand>();
        CreateMap<User, UserDto>();
        CreateMap<SignupCommand, User>();
    }
}