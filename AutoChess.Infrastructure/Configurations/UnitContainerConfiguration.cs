using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AutoChess.Contracts.Interfaces;

namespace AutoChess.Infrastructure.Configurations;

internal class IAutoChessUnitConfiguration : IEntityTypeConfiguration<IUnitContainer>
{
    public void Configure(EntityTypeBuilder<IUnitContainer> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.GameId).IsRequired();
    }
}
