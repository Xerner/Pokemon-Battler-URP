using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AutoChess.Contracts.Models;

namespace AutoChess.Infrastructure.Configurations;

internal class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Username).IsRequired();
        builder.Property(b => b.TrainerSpriteId).IsRequired();
        builder.Property(b => b.TrainerBackgroundId).IsRequired();
        builder.HasOne(b => b.Game).WithMany();
    }
}
