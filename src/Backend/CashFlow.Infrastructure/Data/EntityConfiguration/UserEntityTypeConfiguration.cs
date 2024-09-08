using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashFlow.Infrastructure.Data.EntityConfiguration;
public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id);

        builder
            .Property(e => e.Id)
            .HasColumnName("user_id")
            .HasColumnType("int")
            .UseIdentityColumn();

        builder
            .Property(e => e.UserIdentifier)
            .HasColumnName("user_identifier")
            .HasColumnType("uuid");

        builder
            .Property(e => e.Name)
            .HasColumnName("name")
            .HasColumnType("varchar(50)");

        builder
            .Property(e => e.Email)
            .HasColumnName("email")
            .HasColumnType("varchar(50)");

        builder
            .Property(e => e.Password)
            .HasColumnName("password");

        builder
            .Property(e => e.Role)
            .HasColumnName("role")
            .HasConversion<string>()
            .IsRequired();
    }
}
