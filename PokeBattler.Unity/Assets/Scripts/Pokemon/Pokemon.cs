using System;
using UnityEngine;
using System.Collections.Generic;
using PokeBattler.Unity;

namespace PokeBattler.Core
{
    [Serializable]
    public partial class Pokemon
    {
        

        public int id;
        public string name;
        private string correctedName;
        public int tier;

        public PokemonStat Hp = new PokemonStat() { baseStat = 50, effort = 0 };
        public PokemonStat Attack = new PokemonStat() { baseStat = 50, effort = 0 };
        public PokemonStat Defense = new PokemonStat() { baseStat = 50, effort = 0 };
        public PokemonStat SpecialAttack = new PokemonStat() { baseStat = 50, effort = 0 };
        public PokemonStat SpecialDefense = new PokemonStat() { baseStat = 50, effort = 0 };
        public PokemonStat Speed = new PokemonStat() { baseStat = 50, effort = 0 };

        public PokemonAbility Ability;
        public PokemonMoveSO Move;
        public PokemonMoveSO PPMove;
        private int BaseExperience; // TODO: make use of it, or get rid of it
        private int height; // TODO: make use of it, or get rid of it

        public EPokemonType[] types = new EPokemonType[] { EPokemonType.Normal, EPokemonType.None };

        public List<string> Evolutions;
        public int EvolutionStage;

        public Sprite Sprite;
        public Sprite ShopSprite;
        public Vector2 TrueSpriteSize;

        public string EvolutionsToString()
        {
            string str = "";
            for (int i = 0; i < Evolutions.Count; i++)
            {
                if (i == Evolutions.Count - 1) str += Evolutions[i];
                else str += Evolutions[i] + " → ";
            }
            return str;
        }

        public string TypeToString()
        {
            string str = "";
            for (int i = 0; i < types.Length; i++)
            {
                if (types[i] == EPokemonType.None) continue;
                if (i == types.Length - 1) str += types[i];
                else str += types[i] + "    ";
            }
            return str;
        }

        #region Helper Classes

        [Serializable]
        public class PokemonStat
        {
            public int baseStat;
            public int effort;
        }

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

        #endregion
    }
}
