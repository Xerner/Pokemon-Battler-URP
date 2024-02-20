using UnityEngine;
using PokeBattler.Common.Models;

[CreateAssetMenu(fileName = "New Pokemon", menuName = "PokeBattler/Pokemon")]
public class PokemonSO : ScriptableObject
{
    public Pokemon Pokemon;
}
