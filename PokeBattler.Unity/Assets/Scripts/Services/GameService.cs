using PokeBattler.Common.Models;
using System;

namespace PokeBattler.Client.Services
{
    public interface IGameService
    {
        public Game Game { get; set; }
        Action<Game> OnGameCreated { get; set; }
    }

    public class GameService : IGameService
    {
        public Game Game { get; set; }
        Action<Game> IGameService.OnGameCreated { get; set; }
    }
}