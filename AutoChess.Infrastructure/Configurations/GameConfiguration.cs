using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AutoChess.Contracts.Models;

namespace AutoChess.Infrastructure.Configurations
{
    internal class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(b => b.Id);
            builder.HasMany(b => b.UnitInfos)
                   .WithMany(b => b.Games);
            builder.HasMany(b => b.UnitCounts)
                   .WithOne(b => b.Game);
            builder.Property(b => b.GameOptions).IsRequired();
        }
    }
}
