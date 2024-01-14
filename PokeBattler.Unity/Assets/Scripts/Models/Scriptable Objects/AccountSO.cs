using PokeBattler.Common.Models;
using UnityEngine;

namespace PokeBattler.Unity
{
    [CreateAssetMenu(fileName = "New Account", menuName = "PokemonGO/Account")]
    public class AccountSO : ScriptableObject
    {
        [SerializeField]
        public Account Account = new();
    }
}
