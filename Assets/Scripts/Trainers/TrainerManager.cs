using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainerManager : MonoBehaviour
{
    private static List<Account> Accounts;
    private static HashSet<string> TrainersInGame;
    public GameObject TrainerCardPrefab; // set in the editor

    public static Trainer ActiveTrainer { get; private set; }

    private void Start()
    {
        //Accounts = new List<Account>();
        //if (GameManager.DebugMode)
        //{
            //ActiveTrainer.Account = new Account(new AccountSettings("", 0, 0, ""));
            /*var trainer = global.activeRoom.dashboard.trainers[0]
            trainer.settings[? "username"] = "Ya boy"
            trainer.trainerCard = instance_create_layer(0, 0, "DashboardLayer", TrainerCard)
            trainer.trainerCard.trainer = trainer
            trainer.trainerCard.trainerHeight = sprite_get_height(trainer.settings[? "trainerSprite"])
            global.activeTrainer = trainer
            global.activeRoom.camera.perspective = trainer.arena.perspective
            debug_refresh_shop()*/
        //}
        //TrainersInGame = new HashSet<string>();
    }

    // TODO
    public static void AddTrainer(AccountSettings accountSettings)
    {
        //Account account = new Account(accountSettings); 
        //Trainers.Add()
        //if (TrainersInGame.Contains(account.AccountID)) ;
    }

    //private Account HasTrainer(string id) => Accounts.Find((Account account) => account.AccountID == id);
}
