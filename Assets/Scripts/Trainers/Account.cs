using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Account : MonoBehaviour
{
    //public string AccountID;

    // out of game
    public AccountSettings settings;
    private Sprite TrainerSprite;
    private Sprite TrainerBackground;

    // in game
    public Trainer trainer;

    /// <summary>
    /// Sets the players account settings
    /// </summary>
    /// <param name="username"></param>
    /// <param name="trainerSprite"></param>
    /// <param name="trainerBackground"></param>
    /// <param name="gameID"></param>
    public void SetSettings(string username, string trainerSprite, string trainerBackground, string gameID = null)
    {
        settings.Username = username;
        settings.TrainerSpriteName = trainerSprite;
        settings.TrainerBackgroundName = trainerBackground;
        settings.GameID = gameID ?? settings.GameID;
    }

    /// <summary>
    /// The account class should be a component of the NetworkManager's GameObject
    /// </summary>
    /// <returns>Returns the players Account object</returns>
    public static Account FindAccount() {
        return NetworkManager.Singleton.GetComponent<Account>(); ; ;
    }

    //public AccountSettings SaveSettings() => SaveSystem.SaveAccount(this);

    //public void LoadSettings() => settings = (SaveSystem.LoadAccount());
}
