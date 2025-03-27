using Microsoft.Extensions.DependencyInjection;
using AutoChess.Contracts.Repositories;
using AutoChess.Library.Services;
using AutoChess.Library.Interfaces;

namespace AutoChess.Library.Extensions;

public static class IServiceCollectionExtensions
{
    public static void UseAutoChessServices(this IServiceCollection services)
    {
        services.AddSingleton<IArenaService, ArenaService>();
        services.AddSingleton<IPlayerService, PlayerService>();
        services.AddSingleton<IAccountService, AccountService>();
        services.AddSingleton<IUnitService, UnitService>();
        services.AddSingleton<IGameService, GameService>();
        services.AddSingleton<IShopService, ShopService>();
    }
}
