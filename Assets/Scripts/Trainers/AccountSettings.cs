using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AccountSettings
{
    public string Username;
    public int TrainerSpriteID;
    public int TrainerBackgroundID;
    public string GameID;

    public AccountSettings(string Username, int TrainerSpriteID, int TrainerBackgroundID) : this(Username, TrainerSpriteID, TrainerBackgroundID, "") { }

    public AccountSettings(string Username, int TrainerSpriteID, int TrainerBackgroundID, string GameID)
    {
        this.Username = Username;
        this.TrainerSpriteID = TrainerSpriteID;
        this.TrainerBackgroundID = TrainerBackgroundID;
        this.GameID = GameID;
    }

    public override string ToString()
    {
        return $"{Username}, {TrainerSpriteID}, {TrainerBackgroundID}, {GameID}";
    }
}
