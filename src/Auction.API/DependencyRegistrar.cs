using System.Text;
using Auction.API.Data;
using Auction.API.Data.Interfaces;
using Carter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Azure.Cosmos;
using Microsoft.IdentityModel.Tokens;

namespace Auction.API;

public static class DependencyRegistrar
{
    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<ILotRepository, LotRepository>();
        services.AddScoped<IBidRepository, BidRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }

    public static void ConfigureCosmosDbClient(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var cosmosDbConnectionString = configuration.GetConnectionString("CosmosDb");
        var cosmosDbClientOptions = new CosmosClientOptions
        { 
            AllowBulkExecution = true 
        };

        var cosmosClient = new CosmosClient(cosmosDbConnectionString, cosmosDbClientOptions);
        services.AddSingleton(_ => cosmosClient);
    }

    public static void ConfigureMediatr(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
    }

    public static void ConfigureAutomapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program).Assembly);
    }

    public static void ConfigureCarter(this IServiceCollection services)
    {
        var catalog = new DependencyContextAssemblyCatalog();
        var types = catalog.GetAssemblies().SelectMany(x => x.GetTypes());
        var modules = types
            .Where(t =>
                !t.IsAbstract &&
                typeof(ICarterModule).IsAssignableFrom(t)
                && (t.IsPublic || t.IsNestedPublic)
            ).ToList();
        
        services.AddCarter(configurator: c =>
        {
            c.WithModules(modules.ToArray());
        });
    }

    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(config =>
            {
                config.AllowAnyOrigin();
                config.AllowAnyHeader();
                config.AllowAnyMethod();
            });
        });
    }

    public static void ConfigureJwtAuth(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!))
                };
            });
        services.AddAuthorization();
    }
}