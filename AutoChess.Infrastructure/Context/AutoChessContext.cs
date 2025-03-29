using Microsoft.EntityFrameworkCore;
using AutoChess.Contracts.Interfaces;
using AutoChess.Contracts.Models;

namespace AutoChess.Infrastructure.Context;

public class AutoChessContext(DbContextOptions<AutoChessContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<UnitInfo> UnitInfos { get; set; }
    public DbSet<UnitCount> UnitCounts { get; set; }
    public DbSet<IUnitContainer> UnitContainers { get; set; }
}
