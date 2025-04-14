using AutoChess.Client.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutoChess.Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = new WebHostBuilder();
            builder.UseStartup<Startup>();
            var clientApp = builder.Build();
            clientApp.Run();
        }
    }

    public class Startup : IStartup
    {
        public void Configure(IApplicationBuilder app)
        {
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddAutoChessClient();
            services.AddSingleton<IServer, Server>();
            return services.BuildServiceProvider();
        }
    }

    public class Server : IServer
    {
        public IFeatureCollection Features => new FeatureCollection();

        public void Dispose()
        {
            
        }

        public Task StartAsync<TContext>(IHttpApplication<TContext> application, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
