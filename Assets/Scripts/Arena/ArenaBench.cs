using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBench : MonoBehaviour
{
    public List<PokeContainer> cards;

    public PokeContainer GetAvailableBench()
    {
        foreach (PokeContainer card in cards) if (card.HeldPokemon == null) return card;
        return null;
    }
}
