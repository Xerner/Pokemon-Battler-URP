using Microsoft.Extensions.DependencyInjection;
using AutoChess.Contracts.ExternalApi;
using AutoChess.Infrastructure.ExternalApi;
using AutoChess.Infrastructure.Services;

namespace AutoChess.Infrastructure.Extensions;

public static class IServiceCollectionExtensions
{
    public static void UseHttpInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<HttpService>();
        services.AddSingleton<IPokeApiService, PokeAPIService>();
    }
}
