using PokeBattler.Common.Models;
using UnityEngine;

namespace PokeBattler.Client.Models.SO
{
    [CreateAssetMenu(fileName = "New Game Settings", menuName = "PokemonGO/Game Settings")]
    public class GameSettingsSO : ScriptableObject
    {
        public GameSettings GameSettings;
    }
}