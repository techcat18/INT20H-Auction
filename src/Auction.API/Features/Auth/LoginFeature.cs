using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auction.API.Common.Constants;
using Auction.API.Common.DTOs.Responses.Auth;
using Auction.API.Data.Interfaces;
using Auction.API.Domain.Entities;
using Auction.API.Features.Auth.Commands;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Auction.API.Features.Auth;

public class LoginFeature
{
    internal sealed class Handler : IRequestHandler<LoginCommand, AuthResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public Handler(
            IUserRepository userRepository, 
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);
            if (user is null)
            {
                throw new Exception("User was not found");
            }
            
            if (!VerifyPassword(command.Password, user.PasswordHash))
            {
                throw new Exception("Invalid password");
            }

            var token = GenerateJwtToken(user);
            return new AuthResponseDto
            {
                AccessToken = token
            };
        }
        
        private static bool VerifyPassword(string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }
        
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Name, user.Id),
                    new(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
    
    public class LoginEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost(Constants.ApiEndpoints.Auth.Login, 
                async ([FromBody]LoginCommand loginCommand, ISender sender) =>
                {
                    var authResponse = await sender.Send(loginCommand);
                    return Results.Ok(authResponse);
                });
        }
    }
}