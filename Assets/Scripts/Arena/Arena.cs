using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    // TODO: add class equivalent to GameMakers mp_grid
    public static readonly int Rows = 6;
    public static readonly int Columns = 5;

    [HideInInspector] public Trainer Trainer;
    [HideInInspector] public Trainer EnemyTrainer;
    [HideInInspector] public bool CombatMode;
    private readonly Dictionary<Allegiance, List<Pokemon>> CombatGroups = new Dictionary<Allegiance, List<Pokemon>>();
    public Party Party;
    public GameObject CameraAnchor;
    private ArenaCard[] ArenaCards = new ArenaCard[Rows*Columns];

    public ArenaCard this[Vector2Int index]
    {
        get { 
            if (InBounds(index))
            {
                return null;
            }
            else
            {
                return ArenaCards[index.x + (index.y*Columns)];
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

    public ArenaCard RandomOpenAdjacent(Vector2Int origin)
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
            if (this[adjacent] is ArenaCard card && card.HeldPokemon is null)
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