using System;
using System.Numerics;
using System.Collections.Generic;
using AutoChess.Contracts.Interfaces;
using AutoChess.Contracts.Enums;

namespace AutoChess.Contracts.Models
{
    public class Arena
    {
        public const int Rows = 6;
        public const int Columns = 5;

        public Guid OwnerId = Guid.Empty;
        public Player EnemyTrainer = new();
        public bool CombatMode;
        public Dictionary<EContainerType, IAutoChessUnitContainer[]> PokeContainers { get; private set; } = [];
        public IAutoChessUnitContainer[] Bench { get; private set; } = [];
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

        public ArenaSpot this[Vector2 index]
        {
            get
            {
                if (InBounds(index))
                {
                    return null;
                }
                else
                {
                    return ArenaSpots[(int)(index.X + (index.Y * Columns))];
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

        private bool InBounds(Vector2 index)
        {
            return index.X >= 0 && index.Y < Rows && index.X >= 0 && index.Y < Columns;
        }

        public ArenaSpot RandomOpenAdjacent(Vector2 origin)
        {
            Vector2 adjacent;
            EDirection side = (EDirection)new System.Random().Next(4);
            for (int i = 0; i < 4; i++)
            {
                adjacent = origin;
                switch (side)
                {
                    case EDirection.Top:
                        adjacent.X--;
                        break;
                    case EDirection.Right:
                        adjacent.Y++;
                        break;
                    case EDirection.Bottom:
                        adjacent.X++;
                        break;
                    case EDirection.Left:
                        adjacent.Y--;
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
