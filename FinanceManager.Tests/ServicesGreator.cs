namespace FinanceManager.Tests;

using FinanceManager.Model;
using FinanceManager.Services;
using Microsoft.EntityFrameworkCore;

public class ServicesGreator
{
	private FinanceManagerContext _db;

	public ServicesGreator()
	{
		var optionsBuilder = new DbContextOptionsBuilder<FinanceManagerContext>();
		optionsBuilder.UseSqlServer("Server=localhost;Database=TestFinance;Trusted_Connection=True;Encrypt=False;");

		_db = new FinanceManagerContext(optionsBuilder.Options);
	}

	public OperationService GetOperationService()
	{
		return new OperationService(_db);
	}
	public FinancialOperationService GetFinOperService()
	{
		return new FinancialOperationService(_db);
	}
}
