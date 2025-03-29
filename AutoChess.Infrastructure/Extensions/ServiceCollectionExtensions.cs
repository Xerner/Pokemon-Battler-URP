using Microsoft.Extensions.DependencyInjection;
using AutoChess.Infrastructure.Options;
using AutoChess.Infrastructure.Interfaces;
using AutoChess.Infrastructure.Services;
using Microsoft.Extensions.Configuration;

namespace AutoChess.Infrastructure.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddAutoChessInfrastructure(this IServiceCollection services, Action<ServicesBuilder> configureAction)
    {
        services.AddOptionsWithValidateOnStart<InfrastructureOptions>(InfrastructureOptions.Key);
        var options = services.BuildServiceProvider().GetService<InfrastructureOptions>()!;
        var optionsBuilder = new ServicesBuilder(services, options);
        services.AddSingleton<IJsonService, JsonService>();
        configureAction.Invoke(optionsBuilder);
    }
}
 