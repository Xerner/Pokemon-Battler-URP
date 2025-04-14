using Microsoft.Extensions.DependencyInjection;
using System;

namespace AutoChess.Client.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAutoChessClient(this IServiceCollection services)
        {
            Console.WriteLine("Hello World");
        }
    }
}
