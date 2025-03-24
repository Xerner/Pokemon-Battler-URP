using Microsoft.AspNetCore.SignalR;
using AutoChess.Contracts.Interfaces;
using System;

namespace AutoChess.Server.Extensions;

public static class IHubCallerClientsExtensions
{
    public static T BroadcastToGame<T>(this IHubCallerClients<T> clients, Guid gameId)
    {
        return clients.Group(gameId.ToString());
    }
}
