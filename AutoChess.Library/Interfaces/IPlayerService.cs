﻿using AutoChess.Contracts.Models;

namespace AutoChess.Library.Interfaces;

public interface IPlayerService
{
    Task<bool> AddExperience(Player player, int expToAdd);
    int CalculateInterest(Player trainer);
    Task<Player> CreateOrReconnect(Account account, Game game);
    Task<Player?> GetPlayerAsync(Guid accountId, Guid gameId);
    Task<bool> UpdateTrainerReady(Guid accountId, Guid gameId, bool ready);
}
