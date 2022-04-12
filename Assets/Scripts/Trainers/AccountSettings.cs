using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Serializable class used to load/save account settings
/// </summary>
[Serializable]
public class AccountSettings
{
    public string Username;
    public string TrainerSpriteName;
    public string TrainerBackgroundName;
    public string GameID;

    public AccountSettings(string username, string trainerSpriteName, string trainerBackgroundName) : this(username, trainerSpriteName, trainerBackgroundName, "") { }

    public AccountSettings(string username, string trainerSpriteName, string trainerBackgroundName, string GameID)
    {
        Username = username;
        TrainerSpriteName = trainerSpriteName;
        TrainerBackgroundName = trainerBackgroundName;
        this.GameID = GameID;
    }

    public override string ToString()
    {
        return $"{Username}, {TrainerSpriteName}, {TrainerBackgroundName}, {GameID}";
    }
}
