using AutoChess.Contracts.DTOs;
using AutoChess.Contracts.Models;

namespace AutoChess.Library.Interfaces;

public interface IShopService
{
    public const bool FreeUnits = false; // TODO: app config
    public int GetCost(Pokemon pokemon);
    public Task<UnitClaimedDTO> TryToBuyUnit(Player trainer, Unit unit);
    public BuyExperienceDTO BuyExperience(Player trainer);
    public RefreshShopDTO RefreshShop(Game game, Player player, bool freeShop);
    Task<IEnumerable<Unit>> GetUnitsInShop(Game game, Player player);
}
