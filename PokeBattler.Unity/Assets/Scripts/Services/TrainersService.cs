using Microsoft.AspNetCore.SignalR.Client;
using PokeBattler.Core;
using PokeBattler.Core;
using PokeBattler.Unity;
using PokeBattler.Client.Controllers.HubConnections;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace PokeBattler.Services
{
    public interface ITrainersService
    {
        Trainer[] Trainers { get; }
    }

    public class TrainersService : ITrainersService
    {
        public Trainer[] Trainers { get; private set; }

        private ClientService clientService;

        public Trainer ClientsTrainer
        {
            get => Trainers.FirstOrDefault(trainer_ => trainer_.ClientID == clientService.ClientID);
        }

        public TrainersService(ClientService clientService)
        {
            this.clientService = clientService;
        }

        public TrainersService(ITrainerHubConnection trainerHub, ISignalRService signalr)
        {
            trainerHub.HubConnection.On<ulong, bool>("UpdateTrainerReady", UpdateReadyStatus);
        }

        public async Task UpdateReadyStatus(ulong trainerID, bool ready)
        {

        }

        public void Add(Trainer trainer)
        {
            // if trainer was already in-game and is reconnecting
            var existingTrainer = Trainers.FirstOrDefault(trainer_ => trainer_.ClientID == trainer.ClientID);
            if (existingTrainer != null)
            {
                throw new NotImplementedException();
            }
            // Find next available slot
            int index = Array.FindIndex(Trainers, trainer => trainer == null);
            if (index > -1)
            {
                Trainers[index] = trainer;
                trainer.Arena = GameObject.Find("Arenas").transform.GetChild(index).GetComponent<ArenaBehaviour>();
                TrainerCardManager.Instance.AddTrainerCard(trainer);
                return;
            }
            // Game slots are full
            throw new NotImplementedException();
        }
    }
}
