using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AutoChess.Contracts.ExternalApi;
using AutoChess.Infrastructure.ExternalApi;
using AutoChess.Infrastructure.Interfaces;
using AutoChess.Infrastructure.Services;
using AutoChess.Infrastructure.Context;

namespace AutoChess.Infrastructure.Options;

public class ServicesBuilder(IServiceCollection Services, IAutoChessInfrastructureOptions options)
{
    public void UseHttpInfrastructure()
    {
        Services.AddSingleton<IHttpService, HttpService>();
        Services.AddSingleton<IPokeApiService, PokeAPIService>();
    }

    public void UsePostgreSql()
    {
        Services.AddDbContextPool<AutoChessContext>((provider, optionsBuilder) =>
        {
            optionsBuilder.UseNpgsql(options.ConnectionString);
        });
        Services.AddSingleton<IHttpService, HttpService>();
        Services.AddSingleton<IPokeApiService, PokeAPIService>();
    }
}
