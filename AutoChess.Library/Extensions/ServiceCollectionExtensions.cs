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
        services.AddOptions<GameOptions>(GameOptions.Key);
        services.AddOptions<PoolOptions>(PoolOptions.Key);
        services.AddOptions<ResourceOptions>(ResourceOptions.Key);

        services.AddSingleton<IArenaService, ArenaService>();
        services.AddSingleton<IPlayerService, PlayerService>();
        services.AddSingleton<IAccountService, AccountService>();
        services.AddSingleton<IUnitService, UnitService>();
        services.AddSingleton<IUnitCountService, UnitCountService>();
        services.AddSingleton<IUnitQueryService, UnitQueryService>();
        services.AddSingleton<IUnitContainerService, UnitContainerService>();
        services.AddSingleton<IGameService, GameService>();
        services.AddSingleton<IShopService, ShopService>();
    }
}
