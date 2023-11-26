using PokeBattler.Core;
using PokeBattler.Models;
using UnityEngine;

namespace PokeBattler.Unity
{
    [CreateAssetMenu(fileName = "New Account Settings", menuName = "Pokemon/Account Settings")]
    public class AccountSO : ScriptableObject
    {
        [SerializeField]
        public Account Account;

        void OnValidate()
        {
            if (Account.TrainerSprite != null) 
                Account.Settings.TrainerSpriteId = Account.TrainerSprite.name;
            if (Account.TrainerBackground != null) 
                Account.Settings.TrainerBackgroundId = Account.TrainerBackground.name;
        }
    }
}

