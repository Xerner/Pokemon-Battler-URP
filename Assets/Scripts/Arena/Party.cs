using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour
{
    public List<PokeContainer> cards;

    public PokeContainer NextOpen()
    {
        foreach (PokeContainer card in cards) if (card.HeldPokemon is null) return card;
        return null;
    }
}
