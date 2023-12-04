using PokeBattler.Common;
using System;
using System.Collections.Generic;
using PokeBattler.Common.Models;

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

        readonly IAppConfig appConfig;
        readonly IClientService clientService;

        public Action<Trainer> OnTrainerAdded;

        public TrainersService(IClientService clientService, IAppConfig appConfig)
        {
            this.clientService = clientService;
            this.appConfig = appConfig;
        }

        //public void AddTrainerCard(Trainer trainer, int index)
        //{
        //    trainer.Arena = GameObject.Find("Arenas").transform.GetChild(index).GetComponent<ArenaBehaviour>();
        //    TrainerCardManager.Instance.AddTrainerCard(trainer);
        //}

        //[ServerSubscription("Create")]
        public void TrainerCreated(Trainer trainer)
        {
            OnTrainerAdded.Invoke(trainer);
        }

        //[ServerSubscription("UpdateTrainerReady")]
        public void UpdateReadyStatus(Guid id, bool ready)
        {
            var trainer = Trainers[id];
            if (trainer == null) return;
            trainer.SetReady(ready);
        }
    }
}
