using PokeBattler.Common.Models;
using UnityEngine;

namespace PokeBattler.Unity
{
    [CreateAssetMenu(fileName = "New Account", menuName = "PokemonGO/Account")]
    public class AccountSO : ScriptableObject
    {
        [SerializeField]
        public Account Account;
        public string Username;
        public Sprite TrainerSprite;
        public Sprite TrainerBackground;

        void OnValidate()
        {
            if (Username != null)
                Account.Username = Username;
            if (TrainerSprite != null) 
                Account.TrainerSpriteId = TrainerSprite.name;
            if (TrainerBackground != null) 
                Account.TrainerBackgroundId = TrainerBackground.name;
        }
    }
}

