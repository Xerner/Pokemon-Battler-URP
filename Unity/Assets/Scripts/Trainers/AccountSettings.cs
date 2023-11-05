using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

/// <summary>Serializable class used to load/save account settings</summary>
[Serializable]
public class AccountSettings
{
    public string Username;
    public string TrainerSpriteName;
    public string TrainerBackgroundName;
    /// <summary>The ID given to players in-game so they can have the same username but still be identifiable</summary>
    public ulong ClientID; // we want to save this so when we rejoin a game later, the host knows who we are
    public string GameID; // we want to save this so we can rejoin games later

    public AccountSettings(string username, string trainerSpriteName, string trainerBackgroundName, ulong clientID, string gameID)
    {
        Username = username;
        TrainerSpriteName = trainerSpriteName;
        TrainerBackgroundName = trainerBackgroundName;
        ClientID = clientID;
        GameID = gameID;
    }

    public override string ToString() => $"Username: {Username}\tClient ID: {ClientID}\tGame ID: {GameID}";
}
