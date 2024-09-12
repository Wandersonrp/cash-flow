using CashFlow.Communication.Enums.Expenses;
using CashFlow.Domain.Entities;
using CashFlow.Infrastructure.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.Data.Context;
public class CashFlowDbContext : DbContext
{
    public CashFlowDbContext(DbContextOptions<CashFlowDbContext> options) : base(options)
    {        
    }

    public DbSet<Expense> Expenses { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CashFlowDbContext).Assembly);
        
        modelBuilder.HasPostgresEnum<PaymentType>();

        modelBuilder.ApplyConfiguration(new ExpenseEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
    }
}
