using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AutoChess.Contracts.Models;

namespace AutoChess.Infrastructure.Configurations;

internal class UnitConfiguration : IEntityTypeConfiguration<Unit>
{
    public void Configure(EntityTypeBuilder<Unit> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.InfoId).IsRequired();
        builder.HasOne(b => b.Info).WithMany();
    }
}
