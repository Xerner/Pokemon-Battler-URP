using System;

namespace PokeBattler.Models
{
    /// <summary>Serializable class used to load/save account Settings</summary>
    [Serializable]
    public class AccountSettings
    {
        public string Username;
        public string TrainerSpriteId;
        public string TrainerBackgroundId;
        /// <summary>The ID given to players in-game so they can have the same username but still be identifiable</summary>
        public ulong ClientID; // we want to save this so when we rejoin a game later, the host knows who we are
        public string GameID; // we want to save this so we can rejoin games later

        public AccountSettings(string username, string trainerSpriteID, string trainerBackgroundID, ulong clientID, string gameID)
        {
            Username = username;
            TrainerSpriteId = trainerSpriteID;
            TrainerBackgroundId = trainerBackgroundID;
            ClientID = clientID;
            GameID = gameID;
        }

        public override string ToString() => $"Username: {Username}\tClient ID: {ClientID}\tGame ID: {GameID}";
    }
}
