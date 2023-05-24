namespace FinanceManager.DAL;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class FinanceManagerContextFactory : IDesignTimeDbContextFactory<FinanceManagerContext>
{
    public FinanceManagerContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FinanceManagerContext>();

        ConfigurationBuilder builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");
        IConfigurationRoot config = builder.Build();

        string connectionString = config.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);

        return new FinanceManagerContext(optionsBuilder.Options);
    }
}
