using System;

namespace AutoChess.Contracts.Models {
    [Serializable]
    public class PokemonStats
    {
        int hp;
        int pp;
        int attack;
        int defense;
        int specialAttack;
        int specialDefense;
        float attackSpeed;

        public PokemonStats(Pokemon pokemon)
        {
            hp = pokemon.Hp.BaseStat;
            pp = 100; //pokemon.Pp.baseStat;
            attack = pokemon.Attack.BaseStat;
            defense = pokemon.Defense.BaseStat;
            specialAttack = pokemon.SpecialAttack.BaseStat;
            specialDefense = pokemon.SpecialDefense.BaseStat;
            attackSpeed = pokemon.Speed.BaseStat / 100f;
        }

        public int HP { get => hp; }
        public int PP { get => pp; }
        public int Attack { get => attack; }
        public int Defense { get => defense; }
        public int SpecialAttack { get => specialAttack; }
        public int SpecialDefense { get => specialDefense; }
        public float AttackSpeed { get => attackSpeed; }
    }
}
