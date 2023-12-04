using Microsoft.AspNetCore.SignalR.Client;
using PokeBattler.Client.Services;
using PokeBattler.Common.Models;
using System;
using UnityEngine;

namespace PokeBattler.Client.Controllers
{
    public class TrainerController
    {
        readonly HubConnection connection;
        readonly IClientService clientService;

        public TrainerController(HubConnection connection, IGameService gameService, IClientService clientService, ITrainersService trainersService)
        {
            this.connection = connection;
            this.clientService = clientService;
            gameService.OnGameCreated += AddClientTrainerToGame;
            connection.On<Trainer>("AddToGame", trainersService.TrainerCreated);
            connection.On<Guid, bool>("UpdateTrainerReady", trainersService.UpdateReadyStatus);
        }

        void AddClientTrainerToGame(Game _)
        {
            RequestAddToGame(clientService.Account);
        }

        public void RequestAddToGame(Account account)
        {
            if (connection.State == HubConnectionState.Disconnected) return;
            connection.InvokeAsync<Account>("AddToGame", account);
            Debug.Log($"Requesting to create Trainer with username '{account.Username}'");
        }

        public void RequestUpdateReadyStatus(Guid trainerID, bool ready)
        {
            if (connection.State == HubConnectionState.Disconnected) return;
            connection.InvokeAsync("UpdateReadyStatus", trainerID, ready);
            Debug.Log($"Requesting to update Trainer {trainerID} ready status to {ready}");
        }
    }
}
