using System;
using PokeBattler.Common.Models.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using PokeBattler.Common.Models.Enums;

namespace PokeBattler.Common.Models
{
    public class Arena
    {
        public const int Rows = 6;
        public const int Columns = 5;

        public Guid OwnerId = Guid.Empty;
        public Trainer EnemyTrainer;
        public bool CombatMode;
        public Dictionary<EContainerType, IPokeContainer[]> PokeContainers { get; private set; } = [];
        public IPokeContainer[] Bench { get; private set; } = [];
        public ArenaSpot[] ArenaSpots { get; private set; } = new ArenaSpot[Rows * Columns];
        private readonly Dictionary<EAllegiance, List<Pokemon>> CombatGroups = [];

        public ArenaSpot this[int index]
        {
            get
            {
                if (InBounds(index))
                {
                    return null;
                }
                else
                {
                    return ArenaSpots[index];
                }
            }
        }

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
                    return ArenaSpots[index.x + (index.y * Columns)];
                }
            }
        }

        public void AddPokemon(Pokemon pokemon, EAllegiance allegiance)
        {
            if (CombatGroups[allegiance].IndexOf(pokemon) < 0)
                CombatGroups[allegiance].Add(pokemon);
        }

        public void RemovePokemon(Pokemon pokemon, EAllegiance allegiance)
        {
            if (CombatGroups[allegiance].IndexOf(pokemon) > 0)
                CombatGroups[allegiance].Remove(pokemon);
        }

        private bool InBounds(int index)
        {
            return index >= 0 && index < Rows * Columns;
        }

        private bool InBounds(Vector2Int index)
        {
            return index.y >= 0 && index.y < Rows && index.x >= 0 && index.x < Columns;
        }

        public ArenaSpot RandomOpenAdjacent(Vector2Int origin)
        {
            Vector2Int adjacent;
            EDirection side = (EDirection)new System.Random().Next(4);
            for (int i = 0; i < 4; i++)
            {
                adjacent = origin;
                switch (side)
                {
                    case EDirection.Top:
                        adjacent.x--;
                        break;
                    case EDirection.Right:
                        adjacent.y++;
                        break;
                    case EDirection.Bottom:
                        adjacent.x++;
                        break;
                    case EDirection.Left:
                        adjacent.y--;
                        break;
                    default:
                        throw new Exception("Invalid arena direction");
                }
                if (this[adjacent] is ArenaSpot card && card.Pokemon == null)
                {
                    return card;
                }
                if (side == EDirection.Left)
                {
                    side = EDirection.Top;
                }
                else
                {
                    side++;
                }
            }
            return null;
        }
        
    }
}
