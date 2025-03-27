using System;
using System.Threading.Tasks;
using AutoChess.Contracts.DTOs;
using AutoChess.Contracts.Models;

namespace AutoChess.Contracts.Interfaces
{
    /// <summary>
    /// The API endpoints that are reachable on the Client
    /// </summary>
    public interface IHubClient
    {
        Task AddPlayerToGame(Player player);
        Task PlayerLevelUp(PlayerLevelUpDTO dto);
        Task UnitClaimed(UnitClaimedDTO? dto);
        Task UpdateTrainerReady(Guid accountId, Guid gameId, bool isReady);
    }
}
