using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AutoChess.Contracts.Models;

namespace AutoChess.Infrastructure.Configurations;

internal class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.HasKey(b => new { b.AccountId, b.GameId });
        builder.Property(b => b.Ready).IsRequired();
        builder.Property(b => b.CurrentHealth).IsRequired();
        builder.Property(b => b.TotalHealth).IsRequired();
        builder.Property(b => b.Experience).IsRequired();
        builder.Property(b => b.Money).IsRequired();
        builder.Property(b => b.Level).IsRequired();
        builder.Property(b => b.ExperienceNeededToLevelUp).IsRequired();
}
}
