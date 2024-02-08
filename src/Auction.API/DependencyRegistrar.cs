using Auction.API.Data;
using Auction.API.Data.Interfaces;
using Carter;
using Microsoft.Azure.Cosmos;

namespace Auction.API;

public static class DependencyRegistrar
{
    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<ILotRepository, LotRepository>();
        services.AddScoped<IBidRepository, BidRepository>();
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
}