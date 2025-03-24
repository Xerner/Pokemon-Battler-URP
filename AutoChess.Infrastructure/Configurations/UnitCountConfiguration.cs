using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AutoChess.Contracts.Models;

namespace AutoChess.Infrastructure.Configurations;

public class UnitCountConfiguration : IEntityTypeConfiguration<UnitCount>
{    
    public void Configure(EntityTypeBuilder<UnitCount> builder)
    {
        builder.HasKey(b => new { b.GameId, b.UnitInfoId });
        builder.HasOne(b => b.UnitInfo).WithMany();
        builder.Property(b => b.Count).IsRequired();
        builder.Property(b => b.MaxCount).IsRequired();
    }
}
