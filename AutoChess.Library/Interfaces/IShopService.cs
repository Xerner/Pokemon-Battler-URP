using AutoChess.Contracts.DTOs;
using AutoChess.Contracts.Models;
using AutoChess.Infrastructure.Context;

namespace AutoChess.Library.Interfaces;

public interface IShopService
{
    Task<UnitClaimedDTO?> TryToBuyUnit(Game game, Player player, Unit unit, AutoChessContext context);
    BuyExperienceDTO BuyExperience(Player trainer);
    Task<RefreshShopDTO> RefreshShopAsync(Game game, Player player, bool freeShop, AutoChessContext context);
    Task<IEnumerable<Unit>> GetUnitsInShop(Game game, Player player, AutoChessContext context);
}
