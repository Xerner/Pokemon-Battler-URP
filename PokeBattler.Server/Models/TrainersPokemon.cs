using PokeBattler.Common.Models;
using System.Collections.Generic;

namespace PokeBattler.Server.Models
{
    public class TrainersPokemon : Dictionary<string, List<Pokemon>>
    {
        //Dictionary<string, List<Pokemon>> activePokemon = new Dictionary<string, List<Pokemon>>();

        /// <summary>A list of Pokemon that are active in the game, accessed by their species name</summary>
        //public Dictionary<string, List<Pokemon>> ActivePokemon { get; }
    }
}
