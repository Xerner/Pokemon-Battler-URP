using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AutoChess.Contracts.Interfaces;
using AutoChess.Contracts.DTOs;
using AutoChess.Contracts.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR.Client;

namespace AutoChess.Client.SignalR
{
    public partial class AutoChessServer : IHubServer
    {
        private readonly ILogger<AutoChessServer> Logger;
        private readonly HubConnectionManager Hub;

        public AutoChessServer(ILogger<AutoChessServer> logger, HubConnectionManager hub)
        {
            Logger = logger;
            Hub = hub;
        }

        public async Task<bool> AddToGame(Guid accountId, Guid gameId)
        {
            return await Hub.Connection.InvokeAsync<bool>(nameof(AddToGame), accountId, gameId);
        }

        public async Task<BuyExperienceDTO> BuyExperience(Guid accountId, Guid gameId)
        {
            return await Hub.Connection.InvokeAsync<BuyExperienceDTO>(nameof(BuyExperience), accountId, gameId);
        }

        public async Task<Game> CreateGameAsync(Guid accountId)
        {
            return await Hub.Connection.InvokeAsync<Game>(nameof(CreateGameAsync), accountId);
        }

        public async Task<IEnumerable<int>> GetTierChances(int playerLevel)
        {
            return await Hub.Connection.InvokeAsync<IEnumerable<int>>(nameof(GetTierChances), playerLevel);
        }

        public async Task<string> Ping(string str)
        {
            return await Hub.Connection.InvokeAsync<string>(nameof(Ping), "Pong");
        }

        public async Task<RefreshShopDTO> RefreshShop(Guid accountId, Guid gameId)
        {
            return await Hub.Connection.InvokeAsync<RefreshShopDTO>(nameof(RefreshShop), accountId, gameId);
        }

        public async Task<bool> TryToBuyUnit(Guid accountId, Guid gameId, Guid unitId)
        {
            return await Hub.Connection.InvokeAsync<bool>(nameof(TryToBuyUnit), gameId, unitId);
        }

        public async Task<bool> UpdateTrainerReady(Guid accountId, Guid gameId, bool isReady)
        {
            return await Hub.Connection.InvokeAsync<bool>(nameof(UpdateTrainerReady), accountId, gameId, isReady);
        }
    }
}