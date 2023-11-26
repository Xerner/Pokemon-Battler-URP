using PokeBattler.Core;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PokeBattler.Unity
{
    [AddComponentMenu("Poke Battler/Poke Container")]
    public class PokeContainerBehaviour : MonoBehaviour, IPokeContainer
    {
        PokemonBehaviour pokemon = null;

        public PokemonBehaviour Pokemon { get => pokemon; protected set => pokemon = value; }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (MoveToBehaviour.InstanceOnCursor == null)
            {
                if (Pokemon != null)
                {
                    Pokemon.MoveTo.MoveToCursor();
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
            Pokemon = pokemon;
            return true;
        }

        public void Reset() => SetPokemon(null);
        public void Reset(PokemonBehaviour _) => Reset();
    }

}
