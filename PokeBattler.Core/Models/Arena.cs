using System;
using PokeBattler.Common.Models.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace PokeBattler.Common.Models
{
    public class Arena
    {
        public static readonly int Rows = 6;
        public static readonly int Columns = 5;

        public Guid OwnerId = Guid.Empty;
        public Trainer EnemyTrainer;
        public bool CombatMode;
        private readonly Dictionary<Allegiance, List<Pokemon>> CombatGroups = new Dictionary<Allegiance, List<Pokemon>>();
        public IPokeContainer[] Bench;
        private ArenaSpot[] ArenaCards = new ArenaSpot[Rows * Columns];

        public ArenaSpot this[Vector2Int index]
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

        public void AddPokemon(Pokemon pokemon, Allegiance allegiance)
        {
            if (CombatGroups[allegiance].IndexOf(pokemon) < 0)
                CombatGroups[allegiance].Add(pokemon);
        }

        public void RemovePokemon(Pokemon pokemon, Allegiance allegiance)
        {
            if (CombatGroups[allegiance].IndexOf(pokemon) > 0)
                CombatGroups[allegiance].Remove(pokemon);
        }

        private bool InBounds(Vector2Int index)
        {
            return index.y >= 0 && index.y < Rows && index.x >= 0 && index.x < Columns;
        }

        public ArenaSpot RandomOpenAdjacent(Vector2Int origin)
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
                        throw new Exception("Invalid arena direction");
                }
                if (this[adjacent] is ArenaSpot card && card.Pokemon == null)
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
