using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace AutoChess.Library.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "AutoChess Battler API v1", Version = "v1" });
            options.AddSignalRSwaggerGen();
        });
    }
}
