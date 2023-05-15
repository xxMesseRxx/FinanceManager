namespace FinanceManager.Tests;

using FinanceManager.Model;
using FinanceManager.Services;
using Microsoft.EntityFrameworkCore;

public class ServicesGreator
{
	private FinanceManagerContext _db;
	private string _dbName;

	public ServicesGreator(string dbName)
	{
		_dbName = dbName;

		var optionsBuilder = new DbContextOptionsBuilder<FinanceManagerContext>();
		optionsBuilder.UseSqlServer($"Server=localhost;Database={_dbName};Trusted_Connection=True;Encrypt=False;");

		_db = new FinanceManagerContext(optionsBuilder.Options);
	}

	public OperationService GetOperationService()
	{
		return new OperationService(_db);
	}
	public TransactionService GetTransactionService()
	{
		return new TransactionService(_db);
	}
    public ReportService GetReportService()
    {
        return new ReportService(GetTransactionService());
    }
}
