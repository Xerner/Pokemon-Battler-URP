using Poke.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Poke.Core {
    public class TrainerManager
    {
        private Trainer[] Trainers = new Trainer[8];
        private Dictionary<ulong, int> trainerIndexes = new Dictionary<ulong, int>(); // ulong represents Client IDs
        public static Trainer ActiveTrainer { get => HostBehaviour.Instance.Host.Trainer; }

        //private void Start()
        //{
        //    //Accounts = new List<Account>();
        //    //if (GameSettings.DebugMode)
        //    //{
        //    //ActiveTrainer.Account = new Account(new AccountSettings("", 0, 0, ""));
        //    /*var trainer = global.activeRoom.dashboard.trainers[0]
        //    trainer.Settings[? "username"] = "Ya boy"
        //    trainer.trainerCard = instance_create_layer(0, 0, "DashboardLayer", TrainerCard)
        //    trainer.trainerCard.trainer = trainer
        //    trainer.trainerCard.trainerHeight = sprite_get_height(trainer.Settings[? "trainerSprite"])
        //    global.activeTrainer = trainer
        //    global.activeRoom.camera.perspective = trainer.arena.perspective
        //    debug_refresh_shop()*/
        //    //}
        //    //TrainersInGame = new HashSet<string>();
        //}

        public Trainer Add(Trainer trainer)
        {
            // if trainer was already in-game and is reconnecting
            if (trainerIndexes.ContainsKey(trainer.Account.Settings.ClientID))
            {
                throw new NotImplementedException();
            }
            trainer.OnReady +=
            // Find next available slot
            int index = Array.FindIndex(Trainers, trainer => trainer == null);
            if (index > -1)
            {
                Trainers[index] = trainer;
                trainer.Arena = GameObject.Find("Arenas").transform.GetChild(index).GetComponent<ArenaBehaviour>();
                TrainerCardManager.Instance.AddTrainerCard(trainer);
                return trainer;
            }
            else
            {   
                // Game slots are full
                throw new NotImplementedException();
            }
        }

        public void HandleTrainerReady(ulong clientID)
        {
            int index = trainerIndexes[clientID];
            Trainers[index].Ready = true;
            if (Trainers[index].Ready && Trainers[index].Ready)
            {

            }
        }
    }
}
