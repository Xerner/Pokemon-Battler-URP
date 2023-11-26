using PokeBattler.Models;
using System;
using UnityEngine;

namespace PokeBattler.Models 
{
    [Serializable]
    public class Account
    {
        public AccountSettings Settings;
        public Sprite TrainerSprite;
        public Sprite TrainerBackground;
    }
}
