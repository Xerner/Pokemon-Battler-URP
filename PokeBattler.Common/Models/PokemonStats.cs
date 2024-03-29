using System;

namespace PokeBattler.Common.Models {
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
            hp = pokemon.Hp.baseStat;
            pp = 100; //pokemon.Pp.baseStat;
            attack = pokemon.Attack.baseStat;
            defense = pokemon.Defense.baseStat;
            specialAttack = pokemon.SpecialAttack.baseStat;
            specialDefense = pokemon.SpecialDefense.baseStat;
            attackSpeed = pokemon.Speed.baseStat / 100f;
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
