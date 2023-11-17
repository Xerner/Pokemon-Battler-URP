using Poke.Core;
using UnityEngine;

namespace Poke.Unity
{
    [CreateAssetMenu(fileName = "New Account Settings", menuName = "Pokemon/Account Settings")]
    public class AccountSO : ScriptableObject
    {
        [SerializeField]
        public Account Account;

        void OnValidate()
        {
            Account.Settings.TrainerSpriteId = Account.TrainerSprite?.name;
            Account.Settings.TrainerBackgroundId = Account.TrainerBackground?.name;
        }
    }
}

