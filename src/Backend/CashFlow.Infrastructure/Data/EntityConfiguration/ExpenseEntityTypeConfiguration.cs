using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashFlow.Infrastructure.Data.EntityConfiguration;
internal class ExpenseEntityTypeConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.HasKey(e => e.Id);

        builder
            .Property(e => e.Id)
            .HasColumnName("expense_id")
            .HasColumnType("int");

        builder
            .Property(e => e.Title)
            .HasColumnName("title")
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder
            .Property(e => e.Description)
            .HasColumnName("description")
            .HasColumnType("varchar(50)");

        builder
            .Property(e => e.Amount)
            .HasColumnName("amount")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder
            .Property(e => e.Date)
            .HasColumnName("date")
            .HasColumnType("timestamp with time zone");

        builder
            .Property(e => e.PaymentType)
            .HasColumnName("payment_type")
            .HasConversion<string>()
            .IsRequired();

        builder
            .HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId);

        builder
            .Property(e => e.UserId)
            .HasColumnName("user_id")
            .HasColumnType("int")
            .IsRequired();
    }
}
