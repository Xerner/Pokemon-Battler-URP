using UnityEngine;

namespace Poke.Unity
{
    [AddComponentMenu("Poke Battler/Arena Spot")]
    public class ArenaSpotBehaviour : PokeContainerBehaviour
    {
        public static readonly Vector2Int UnitVector = new Vector2Int(128, 128);

        public Vector2Int Index;
        public Allegiance Allegiance;
        public PokemonBehaviour CombatPokemon;

        public override bool SetPokemon(PokemonBehaviour pokemon)
        {
            Pokemon = pokemon;

            // Move pokemon
            if (pokemon != null)
            {
                pokemon.MoveTo.MoveTo(transform, true);
                return false;
            }
            return true;
        }
    }
}
