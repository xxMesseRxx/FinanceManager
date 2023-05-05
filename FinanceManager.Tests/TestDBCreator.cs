namespace FinanceManager.Tests;

using FinanceManager.Model;
using Microsoft.EntityFrameworkCore;

public class TestDBCreator : IDisposable
{
	private FinanceManagerContext? _db;

	public void CreateTestDB()
	{
		var optionsBuilder = new DbContextOptionsBuilder<FinanceManagerContext>();
		optionsBuilder.UseSqlServer("Server=localhost;Database=TestUniversity;Trusted_Connection=True;Encrypt=False;");

		_db = new FinanceManagerContext(optionsBuilder.Options);

		_db?.Database.EnsureCreated();
	}
	public void Dispose()
	{
		_db?.Database.EnsureDeleted();
	}
}

