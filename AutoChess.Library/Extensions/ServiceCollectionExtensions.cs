using Microsoft.Extensions.DependencyInjection;
using AutoChess.Contracts.Repositories;
using AutoChess.Library.Services;
using AutoChess.Library.Interfaces;
using AutoChess.Contracts.Options;

namespace AutoChess.Library.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddAutoChessServices(this IServiceCollection services)
    {
        services.AddOptionsWithValidateOnStart<IGameOptions>(IGameOptions.Key);
        services.AddOptionsWithValidateOnStart<IPoolOptions>(IPoolOptions.Key);
        services.AddOptionsWithValidateOnStart<IResourceOptions>(IResourceOptions.Key);
        services.AddSingleton<IArenaService, ArenaService>();
        services.AddSingleton<IPlayerService, PlayerService>();
        services.AddSingleton<IAccountService, AccountService>();
        services.AddSingleton<IUnitService, UnitService>();
        services.AddSingleton<IGameService, GameService>();
        services.AddSingleton<IShopService, ShopService>();
    }
}
