using PokeBattler.Common;
using System;
using System.Collections.Generic;
using PokeBattler.Common.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace PokeBattler.Client.Services
{
    public interface ITrainersService
    {
        Trainer ClientsTrainer { get; }
        Dictionary<Guid, Trainer> Trainers { get; }
        void TrainerCreated(Trainer trainer);
        void UpdateReadyStatus(Guid id, bool ready);
    }

    public partial class TrainersService : ITrainersService
    {
        public Trainer ClientsTrainer { get => Trainers[clientService.ClientID]; }
        public Dictionary<Guid, Trainer> Trainers { get; private set; }

        readonly HubConnection connection;
        readonly IAppConfig appConfig;
        readonly IClientService clientService;

        public Action<Trainer> OnTrainerAdded;

        public TrainersService(HubConnection connection, 
                               IClientService clientService, 
                               IAppConfig appConfig, 
                               IGameService gameService, 
                               ITrainersService trainersService)
        {
            this.connection = connection;
            this.clientService = clientService;
            this.appConfig = appConfig;
            gameService.OnGameCreated += AddClientTrainerToGame;
            connection.On<Trainer>("AddToGame", trainersService.TrainerCreated);
            connection.On<Guid, bool>("UpdateTrainerReady", trainersService.UpdateReadyStatus);
        }

        //public void AddTrainerCard(Trainer trainer, int index)
        //{
        //    trainer.Arena = GameObject.Find("Arenas").transform.GetChild(index).GetComponent<ArenaBehaviour>();
        //    TrainerCardManager.Instance.AddTrainerCard(trainer);
        //}

        public void TrainerCreated(Trainer trainer)
        {
            OnTrainerAdded.Invoke(trainer);
        }

        public void UpdateReadyStatus(Guid id, bool ready)
        {
            var trainer = Trainers[id];
            if (trainer == null) return;
            trainer.SetReady(ready);
        }

        void AddClientTrainerToGame(Game _)
        {
            RequestAddToGame(clientService.Account);
        }

        public void RequestAddToGame(Account account)
        {
            if (connection.State == HubConnectionState.Disconnected) return;
            connection.InvokeAsync<Account>("AddToGame", account);
            Debug2.Log($"Requesting to create Trainer with username '{account.Username}'");
        }

        public void RequestUpdateReadyStatus(Guid trainerID, bool ready)
        {
            if (connection.State == HubConnectionState.Disconnected) return;
            connection.InvokeAsync("UpdateReadyStatus", trainerID, ready);
            Debug2.Log($"Requesting to update Trainer {trainerID} ready status to {ready}");
        }
    }
}
