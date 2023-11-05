using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonMoveBehaviour : MonoBehaviour
{
    public PokemonMove Move;
    public PokemonBehaviour Source;
    public PokemonBehaviour Target;
    int damageFrame;
    int repeatFrame;
    int restartFrame;
    int currentFrame;
    int totalFrames;

    // Logic was lightly copy/pasted from GameMaker. needs to be Unityfied
    void Update() {
        if (isDoneAnimating()) Destroy(gameObject);
    }

    bool isDoneAnimating() => currentFrame > totalFrames - 1;
}
