using PokeBattler.Common.Models;
using PokeBattler.Common.Models.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PokeBattler.Unity
{
    [AddComponentMenu("Poke Battler/Poke Container")]
    public class PokeContainerBehaviour : MonoBehaviour, IPokeContainer
    {
        public PokemonBehaviour PokemonGO { get; protected set; }
        Pokemon IPokeContainer.Pokemon { get => PokemonGO.pokemon; set => PokemonGO.pokemon = value; }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (MoveToBehaviour.InstanceOnCursor == null)
            {
                if (PokemonGO != null)
                {
                    PokemonGO.MoveTo.MoveToCursor();
                    SetPokemon(null);
                }
                return;
            }
            if (MoveToBehaviour.InstanceOnCursor.TryGetComponent(out PokemonBehaviour pokemon))
            {
                pokemon.SetPokeContainer(this);
            }
        }

        public virtual bool SetPokemon(PokemonBehaviour pokemon)
        {
            if (pokemon != null)
            {
                pokemon.OnDestroyed += Reset;
            }
            PokemonGO = pokemon;
            return true;
        }

        public void Reset() => SetPokemon(null);
        public void Reset(PokemonBehaviour _) => Reset();
    }

}
