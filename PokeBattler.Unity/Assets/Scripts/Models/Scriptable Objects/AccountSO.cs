using PokeBattler.Common.Models;
using UnityEngine;

namespace PokeBattler.Unity
{
    [CreateAssetMenu(fileName = "New Account", menuName = "PokeBattler/Account")]
    public class AccountSO : ScriptableObject
    {
        [SerializeField]
        public Account Account = new();
    }
}
