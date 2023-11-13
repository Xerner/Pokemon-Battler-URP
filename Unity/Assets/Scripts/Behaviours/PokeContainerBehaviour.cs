using Poke.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Poke.Unity
{
    [AddComponentMenu("Poke Battler/Poke Container")]
    public abstract class PokeContainerBehaviour : MonoBehaviour, IPokeContainer
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
                pokemon.SetPokeContainer();
            }
        }

        public abstract bool SetPokemon(PokemonBehaviour pokemon);

        public void Reset() => SetPokemon(null);
    }

}
