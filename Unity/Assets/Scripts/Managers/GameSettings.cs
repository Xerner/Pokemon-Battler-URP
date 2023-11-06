using UnityEngine;

[CreateAssetMenu(fileName = "New Game Settings", menuName = "Pokemon/Game Settings")]
public class GameSettings : ScriptableObject
{
    public bool FreeRefreshShop = false;
    public bool FreeExperience = false;
    public bool FreePokemon = false;
}
