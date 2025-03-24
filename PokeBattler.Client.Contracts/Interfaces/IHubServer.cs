using System;
using System.Threading.Tasks;
using AutoChess.Contracts.DTOs;
using AutoChess.Contracts.Models;

namespace AutoChess.Contracts.Interfaces
{
    /// <summary>
    /// The API endpoints that are reachable on the Server
    /// </summary>
    public interface IHubServer
    {
        Task<string> Ping(string str);
        Task<Game> CreateGameAsync();
        Task AddToGame(Account account);
        Task<bool> UpdateTrainerReady(Guid accountId, Guid gameId, bool isReady);
        Task<int[]> GetTierChances(int playerLevel);
        Task<BuyExperienceDTO> BuyExperience(Guid id);
        Task ClaimUnit(Guid accountId, Guid gameId, Guid unitId);
        Task<RefreshShopDTO> RefreshShop(Guid id);
    }
}
