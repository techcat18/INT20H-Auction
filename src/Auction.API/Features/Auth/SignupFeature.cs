using Auction.API.Common.Constants;
using Auction.API.Common.DTOs.Responses.Auth;
using Auction.API.Data.Interfaces;
using Auction.API.Domain.Entities;
using Auction.API.Features.Auth.Commands;
using AutoMapper;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auction.API.Features.Auth;

public class SignupFeature
{
    internal sealed class Handler : IRequestHandler<SignupCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public Handler(
            IUserRepository userRepository, 
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(SignupCommand command, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);
            if (existingUser is not null)
            {
                throw new Exception("User with this email already exists");
            }

            var hashedPassword = HashPassword(command.Password);

            var user = _mapper.Map<User>(command);
            user.PasswordHash = hashedPassword;

            await _userRepository.CreateAsync(user, user.Email, cancellationToken);

            return _mapper.Map<UserDto>(user);
        }
        
        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
        }
    }
    
    public class SignupEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost(Constants.ApiEndpoints.Auth.Signup, 
                async ([FromBody]SignupCommand signupCommand, ISender sender) =>
                {
                    var user = await sender.Send(signupCommand);
                    return Results.Ok(user);
                });
        }
    }
}