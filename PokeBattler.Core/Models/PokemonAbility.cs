using System;

namespace PokeBattler.Common.Models
{
    [Serializable]
    public class PokemonAbility
    {
        public string name;
        public string description;
        public string longDescription;
        public string url;
        public bool isHidden;
        public int slot;
    }
}
