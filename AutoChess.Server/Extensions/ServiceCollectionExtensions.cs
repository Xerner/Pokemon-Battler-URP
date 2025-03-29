using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;
using System;

namespace AutoChess.Library.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "AutoChess Battler API v1", Version = "v1" });
            //var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

            //options.IncludeXmlComments(xmlCommentsFullPath);
            options.AddSignalRSwaggerGen(signalrOptions =>
            {
                //signalrOptions.AutoDiscover = SignalRSwaggerGen.Enums.AutoDiscover.MethodsAndParams;
                //signalrOptions.DisableSecurity = true;
                //signalrOptions.UseHubXmlCommentsSummaryAsTagDescription = true;
                //signalrOptions.UseHubXmlCommentsSummaryAsTag = true;
                //signalrOptions.UseXmlComments(xmlCommentsFullPath);
            });
            options.AddSignalRSwaggerGen();
        });
    }
}
