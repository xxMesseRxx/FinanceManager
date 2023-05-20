namespace FinanceManager.DAL;

using FinanceManager.Model;
using Microsoft.EntityFrameworkCore;

public class FinanceManagerContext : DbContext
{
    public DbSet<Operation> Operations { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;

    public FinanceManagerContext(DbContextOptions<FinanceManagerContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Operation>(entity =>
        {
            entity.HasKey(o => o.Id).HasName("PK_Operations_Id");
            entity.HasIndex(o => o.Name, "UQ_Operations_Name").IsUnique();
            entity.Property(o => o.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(t => t.Id).HasName("PK_Transactions_Id");
            entity.Property(t => t.DateTime).HasColumnType("datetime");
        });
    }
}
