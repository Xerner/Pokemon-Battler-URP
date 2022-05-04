using System;
using System.Collections.Generic;
using UnityEngine;

public class TrainerManager {
    private static Trainer[] Trainers = new Trainer[8];
    private static Dictionary<ulong, int> trainerIndexes = new Dictionary<ulong, int>(); // ulong represents Client IDs
    public static Trainer ActiveTrainer { get; private set; }

    public static void SetActiveTrainer(Trainer activeTrainer) {
        ActiveTrainer = activeTrainer;
        Dashboard.Instance.Level = activeTrainer.Level.ToString();
        Dashboard.Instance.Experience = activeTrainer.Experience.ToString() + "/" + Trainer.ExpToNextLevel[activeTrainer.Level];
        Dashboard.Instance.Money = activeTrainer.Money.ToString();
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

    public static Trainer Add(Trainer trainer)
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
