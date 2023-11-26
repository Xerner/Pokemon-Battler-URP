using UnityEngine;

namespace PokeBattler.Unity
{
    public class PokemonMoveBehaviour : MonoBehaviour
    {
        public PokemonMoveSO Move;
        public PokemonBehaviour Source;
        public PokemonBehaviour Target;
        int damageFrame;
        int repeatFrame;
        int restartFrame;
        int currentFrame;
        int totalFrames;

        // Logic was lightly copy/pasted from GameMaker. needs to be Unityfied
        void Update()
        {
            if (isDoneAnimating()) Destroy(gameObject);
        }

        bool isDoneAnimating() => currentFrame > totalFrames - 1;
    }
}
