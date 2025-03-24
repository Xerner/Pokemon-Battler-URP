using System;

namespace AutoChess.Contracts.Models 
{ 
    [Serializable]
    public class Account
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = "";
        public string TrainerSpriteId { get; set; } = "";
        public string TrainerBackgroundId { get; set; } = "";
        /// <summary>The Id given to players in-game so they can have the same username but still be identifiable</summary>
        public Guid GameId; // we want to save this so we can rejoin games later
        public Game Game { get; set; }
        public override string ToString() => $"Username: {Username}\tClient Id: {Id}\tGame Id: {GameId}";
    }
}
