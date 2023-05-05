namespace FinanceManager.Tests;

using FinanceManager.Model;
using Microsoft.EntityFrameworkCore;

public class TestDBCreator : IDisposable
{
	private FinanceManagerContext? _db;

	public void CreateTestDB()
	{
		var optionsBuilder = new DbContextOptionsBuilder<FinanceManagerContext>();
		optionsBuilder.UseSqlServer("Server=localhost;Database=TestFinance;Trusted_Connection=True;Encrypt=False;");

		_db = new FinanceManagerContext(optionsBuilder.Options);

		_db?.Database.EnsureCreated();
		FillDbWithTestData();
	}
	public void Dispose()
	{
		_db?.Database.EnsureDeleted();
	}

	private void FillDbWithTestData()
	{
		var operations = CreateTestOperations();
		_db.Operations.AddRange(operations);
		_db.SaveChanges();

		var finOperations = CreateTestFinOper(operations);
		_db.FinancialOperations.AddRange(finOperations);
		_db.SaveChanges();
	}
	private List<Operation> CreateTestOperations()
	{
		List<Operation> operations = new List<Operation>()
		{
			new Operation() { Type = "Оплата квартиры" },
			new Operation() { Type = "Оплата курсов" },
			new Operation() { Type = "Зарплата" },
			new Operation() { Type = "Долг" }
		};

		return operations;
	}
	private List<FinancialOperation> CreateTestFinOper(List<Operation> operations)
	{
		List<FinancialOperation> finOperations = new List<FinancialOperation>()
		{
			new FinancialOperation() { Sum = -2000000, DateTime = DateTime.Now, OperationTypeId = operations[0].Id },
			new FinancialOperation() { Sum = -1000000, DateTime = DateTime.Now, OperationTypeId = operations[1].Id, Discription = "English" },
			new FinancialOperation() { Sum = -500000, DateTime = DateTime.Now, OperationTypeId = operations[1].Id, Discription = "Math" },
			new FinancialOperation() { Sum = 2000000, DateTime = DateTime.Now, OperationTypeId = operations[2].Id },
			new FinancialOperation() { Sum = 2000000, DateTime = DateTime.Now, OperationTypeId = operations[2].Id }
		};

		return finOperations;
	}
}

