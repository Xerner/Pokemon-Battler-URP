using System;

namespace AutoChess.Contracts.Models;

public class UnitCount
{
    public Guid GameId { get; set; }
    public Guid UnitInfoId { get; set; }
    public int Count { get; set; }
    public int MaxCount { get; set; }
    public UnitInfo UnitInfo { get; set; }
    public Game Game { get; set; }
}
