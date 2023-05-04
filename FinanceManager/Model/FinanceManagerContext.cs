namespace FinanceManager.Model;

using Microsoft.EntityFrameworkCore;

public class FinanceManagerContext : DbContext
{
	public DbSet<Operation> Operations { get; set; } = null!;
    public DbSet<FinancialOperation> FinancialOperations { get; set; } = null!;

    public FinanceManagerContext(DbContextOptions<FinanceManagerContext> options)
		: base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Operation>(entity =>
		{
			entity.HasKey(o => o.Id).HasName("PK_Operations_Id");
			entity.HasIndex(o => o.Type, "UQ_Operations_Type").IsUnique();
			entity.Property(o => o.Type).HasMaxLength(100);
		});

		modelBuilder.Entity<FinancialOperation>(entity =>
		{
			entity.HasKey(f => f.Id).HasName("PK_FinancialOperations_Id");
			entity.HasOne(f => f.OperationType).WithMany(o => o.FinancialOperations)
				.HasForeignKey(f => f.OperationTypeId)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("FK_FinancialOperation_Operations");
		});
	}
}
