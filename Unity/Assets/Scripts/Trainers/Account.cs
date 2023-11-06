using Poke.Serializable;
using System;
using UnityEngine;

namespace Poke.Core {
    [Serializable]
    public class Account
    {
        public AccountSettings Settings;
        public Sprite TrainerSprite;
        public Sprite TrainerBackground;
    }
}
