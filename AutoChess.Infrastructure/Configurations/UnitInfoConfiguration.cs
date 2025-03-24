using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AutoChess.Contracts.Models;

namespace AutoChess.Infrastructure.Configurations;

internal class UnitInfoConfiguration : IEntityTypeConfiguration<UnitInfo>
{
    public void Configure(EntityTypeBuilder<UnitInfo> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name).IsRequired();
        builder.Property(b => b.Tier).IsRequired();
    }
}
