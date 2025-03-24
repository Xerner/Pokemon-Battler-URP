using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using AutoChess.Contracts.Interfaces;
using AutoChess.Contracts.Repositories;
using AutoChess.Library.Services;

namespace AutoChess.Library.Extensions;

public static class IServiceCollectionExtensions
{
    public static void UseAutoChessServices(this IServiceCollection services)
    {
        services.AddSingleton<IArenaService, ArenaService>();
        services.AddSingleton<ITrainersService, PlayerService>();
        services.AddSingleton<IAccountService, AccountService>();
        services.AddSingleton<IUnitService, UnitService>();
        services.AddSingleton<IGameService, GameService>();
        services.AddSingleton<IShopService, ShopService>();
    }
}
