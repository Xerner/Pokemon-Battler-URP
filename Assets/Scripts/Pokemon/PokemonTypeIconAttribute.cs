using UnityEngine;
using static Pokemon;

/// <summary>
/// Custom inspector property icon.
/// </summary>
public class PokemonTypeIconAttribute : PropertyAttribute {

    /// <summary>
    /// Custom inspector property icon.
    /// </summary>
    /// <param name="path">The relative path (starting from 'Assets/') to the icon you want to display in front of the property.</param>
    public PokemonTypeIconAttribute() {
    }
}
