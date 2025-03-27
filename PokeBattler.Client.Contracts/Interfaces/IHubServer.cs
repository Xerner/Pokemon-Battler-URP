using System;
using System.Collections.Generic;
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
        string Ping(string str);
        Task<Game?> CreateGameAsync(Guid accountId);
        Task AddToGame(Guid accountId, Guid gameId);
        Task<bool> UpdateTrainerReady(Guid accountId, Guid gameId, bool isReady);
        IEnumerable<int> GetTierChances(int playerLevel);
        Task<BuyExperienceDTO?> BuyExperience(Guid accountId, Guid gameId);
        Task TryToBuyUnit(Guid accountId, Guid gameId, Guid unitId);
        Task<RefreshShopDTO?> RefreshShop(Guid accountId, Guid gameId);
    }
}
