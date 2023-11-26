using UnityEngine;

namespace PokeBattler.Unity
{
    [AddComponentMenu("Poke Battler/Arena Spot")]
    [RequireComponent(typeof(Collider))]
    public class ArenaSpotBehaviour : PokeContainerBehaviour
    {
        public static readonly Vector2Int UnitVector = new Vector2Int(128, 128);

        public Vector2Int Index;
        public Allegiance Allegiance;
        public PokemonBehaviour CombatPokemon;

        public override bool SetPokemon(PokemonBehaviour pokemon)
        {
            base.SetPokemon(pokemon);
            if (pokemon == null)
                return false;

            pokemon.MoveTo.MoveTo(transform, true);
            return false;
        }
    }
}
