using System;
using System.Collections.Generic;
using AutoChess.Contracts.Enums;

namespace AutoChess.Contracts.Models
{
    [Serializable]
    public class Pokemon
    {
        public Guid Id = Guid.Empty;
        public int PokeId = 0;
        public string Name = "";
        public string CorrectedName = "";
        public int Tier = 0;
        public int EvolutionStage;
        public int BaseExperience; // TODO: make use of it, or get rid of it
        public int Height; // TODO: make use of it, or get rid of it

        public PokemonStat Hp = new() { BaseStat = 50, Effort = 0 };
        public PokemonStat Attack = new() { BaseStat = 50, Effort = 0 };
        public PokemonStat Defense = new() { BaseStat = 50, Effort = 0 };
        public PokemonStat SpecialAttack = new() { BaseStat = 50, Effort = 0 };
        public PokemonStat SpecialDefense = new() { BaseStat = 50, Effort = 0 };
        public PokemonStat Speed = new() { BaseStat = 50, Effort = 0 };

        public PokemonAbility? Ability;
        public PokemonMove? Move;
        public PokemonMove? PPMove;

        public EPokemonType[] Types = [EPokemonType.Normal, EPokemonType.None];

        public List<string> Evolutions  = [];
        public byte[] Sprite = [];
        public byte[] ShopSprite = [];
        public Vector2Int TrueSpriteSize = new();
    }
}
