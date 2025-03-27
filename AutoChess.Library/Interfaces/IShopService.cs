using AutoChess.Contracts.DTOs;
using AutoChess.Contracts.Models;

namespace AutoChess.Library.Interfaces;

public interface IShopService
{
    Task<UnitClaimedDTO?> TryToBuyUnit(Game game, Player player, Unit unit);
    BuyExperienceDTO BuyExperience(Player trainer);
    Task<RefreshShopDTO> RefreshShopAsync(Game game, Player player, bool freeShop);
    Task<IEnumerable<Unit>> GetUnitsInShop(Game game, Player player);
}
