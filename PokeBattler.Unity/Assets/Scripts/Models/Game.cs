using PokeBattler.Core;
using System;
using System.Collections.Generic;

namespace PokeBattler.Models
{
    public class Game
    {
        public PokemonPool PokemonPool;
        public Timer RoundTimer = new();
        public GameSettings GameSettings;
    }
}