using System;
using System.Collections.Generic;
using UnityEngine;

public class TrainerManager {
    private static TrainerManager instance = null;
    private static readonly object padlock = new object();

    public static TrainerManager Instance {
        get {
            lock (padlock) {
                // Instance is supposed to be created upon game creation in, maybe, PokeHost
                //if (instance == null) instance = new TrainerManager(); 
                return instance;
            }
        }
        set {
            instance = value;
        }
    }


    private Trainer[] Trainers = new Trainer[8];
    private Dictionary<ulong, int> trainerIndexes = new Dictionary<ulong, int>(); // ulong represents Client IDs
    public Trainer ActiveTrainer { get; private set; }

    public TrainerManager(Trainer activeTrainer) {
        ActiveTrainer = activeTrainer;
    }

    //private void Start()
    //{
    //    //Accounts = new List<Account>();
    //    //if (GameManager.DebugMode)
    //    //{
    //    //ActiveTrainer.Account = new Account(new AccountSettings("", 0, 0, ""));
    //    /*var trainer = global.activeRoom.dashboard.trainers[0]
    //    trainer.settings[? "username"] = "Ya boy"
    //    trainer.trainerCard = instance_create_layer(0, 0, "DashboardLayer", TrainerCard)
    //    trainer.trainerCard.trainer = trainer
    //    trainer.trainerCard.trainerHeight = sprite_get_height(trainer.settings[? "trainerSprite"])
    //    global.activeTrainer = trainer
    //    global.activeRoom.camera.perspective = trainer.arena.perspective
    //    debug_refresh_shop()*/
    //    //}
    //    //TrainersInGame = new HashSet<string>();
    //}

    public Trainer Add(Trainer trainer)
    {
        if (trainerIndexes.ContainsKey(trainer.Account.settings.ClientID)) {
            // TODO: if trainer was already in-game and is reconnecting
            return null;
        }
        // Find next available slot
        int index = Array.FindIndex(Trainers, trainer => trainer == null);
        if (index > 0) {
            Trainers[index] = trainer;
            TrainerCardManager.Instance.AddTrainerCard(trainer, index);
            return trainer;
        } else {
            // TODO: Game slots are full
        }
        return null;
    }
}
