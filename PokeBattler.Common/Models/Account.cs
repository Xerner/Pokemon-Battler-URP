using System;

namespace PokeBattler.Common.Models 
{ 
    [Serializable]
    public class Account
    {
        public string Username { get; set; }
        public string TrainerSpriteId { get; set; }
        public string TrainerBackgroundId { get; set; }
        /// <summary>The Id given to players in-game so they can have the same username but still be identifiable</summary>
        public Guid Id { get; set; } // we want to save this so when we rejoin a game later, the host knows who we are
        public Guid GameId { get; set; } // we want to save this so we can rejoin games later
        public override string ToString() => $"Username: {Username}\tClient Id: {Id}\tGame Id: {GameId}";
    }
}
