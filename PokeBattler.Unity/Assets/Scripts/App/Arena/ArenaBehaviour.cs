using PokeBattler.Core;
using System.Collections.Generic;
using UnityEngine;

namespace PokeBattler.Unity
{
    [AddComponentMenu("Poke Battler/Arena")]
    public class ArenaBehaviour : MonoBehaviour
    {
        public static readonly int Rows = 6;
        public static readonly int Columns = 5;

        [HideInInspector] public Trainer Trainer;
        [HideInInspector] public Trainer EnemyTrainer;
        [HideInInspector] public bool CombatMode;
        private readonly Dictionary<Allegiance, List<PokemonBehaviour>> CombatGroups = new Dictionary<Allegiance, List<PokemonBehaviour>>();
        public BenchesBehaviour Bench;
        private ArenaSpotBehaviour[] ArenaCards = new ArenaSpotBehaviour[Rows * Columns];

        public ArenaSpotBehaviour this[Vector2Int index]
        {
            get
            {
                if (InBounds(index))
                {
                    return null;
                }
                else
                {
                    return ArenaCards[index.x + (index.y * Columns)];
                }
            }
        }

        public void AddPokemon(PokemonBehaviour pokemon, Allegiance allegiance)
        {
            if (CombatGroups[allegiance].IndexOf(pokemon) < 0)
                CombatGroups[allegiance].Add(pokemon);
        }

        public void RemovePokemon(PokemonBehaviour pokemon, Allegiance allegiance)
        {
            if (CombatGroups[allegiance].IndexOf(pokemon) > 0)
                CombatGroups[allegiance].Remove(pokemon);
        }

        private bool InBounds(Vector2Int index)
        {
            return index.y >= 0 && index.y < Rows && index.x >= 0 && index.x < Columns;
        }

        public ArenaSpotBehaviour RandomOpenAdjacent(Vector2Int origin)
        {
            Vector2Int adjacent;
            Direction side = (Direction)new System.Random().Next(4);
            for (int i = 0; i < 4; i++)
            {
                adjacent = origin;
                switch (side)
                {
                    case Direction.Top:
                        adjacent.x--;
                        break;
                    case Direction.Right:
                        adjacent.y++;
                        break;
                    case Direction.Bottom:
                        adjacent.x++;
                        break;
                    case Direction.Left:
                        adjacent.y--;
                        break;
                    default:
                        throw new System.Exception("Invalid arena direction");
                }
                if (this[adjacent] is ArenaSpotBehaviour card && card.Pokemon == null)
                {
                    return card;
                }
                if (side == Direction.Left)
                {
                    side = Direction.Top;
                }
                else
                {
                    side++;
                }
            }
            return null;
        }
        private enum Direction
        {
            Right = 0,
            Bottom = 1,
            Left = 2,
            Top = 3,
        }
    }

    public enum Allegiance
    {
        Ally,
        Enemy
    }
}