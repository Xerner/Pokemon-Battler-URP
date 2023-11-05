using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

/// <summary>The Account class is initialized or created before the player joins a game</summary>
[Serializable]
public class Account
{
    // out of game
    public AccountSettings settings;
    [SerializeField] private Sprite TrainerSprite;
    [SerializeField] private Sprite TrainerBackground;

    /// <summary>Sets the players account settings</summary>
    public Account(string username, string trainerSprite, string trainerBackground, string gameID = null)
    {
        settings.Username = username;
        settings.TrainerSpriteName = trainerSprite;
        settings.TrainerBackgroundName = trainerBackground;
        settings.GameID = gameID ?? settings.GameID;
    }

    /// <summary>Sets the players account settings</summary>
    public Account(AccountSettings settings) {
        this.settings = settings;
    }
}
