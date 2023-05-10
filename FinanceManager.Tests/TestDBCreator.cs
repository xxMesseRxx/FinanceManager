namespace FinanceManager.Tests;

using FinanceManager.Model;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

public class TestDBCreator : IDisposable
{
	public string DbName { get; private set; }

	private FinanceManagerContext? _db;

	public void CreateTestDB()
	{
		DbName = Guid.NewGuid().ToString();

		var optionsBuilder = new DbContextOptionsBuilder<FinanceManagerContext>();
		optionsBuilder.UseSqlServer($"Server=localhost;Database={DbName};Trusted_Connection=True;Encrypt=False;");

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

		var transactions = CreateTestTransactions(operations);
		_db.Transactions.AddRange(transactions);
		_db.SaveChanges();
	}
	private List<Operation> CreateTestOperations()
	{
		List<Operation> operations = new List<Operation>()
		{
			new Operation() { Name = "Оплата квартиры" },
			new Operation() { Name = "Оплата курсов" },
			new Operation() { Name = "Зарплата" },
			new Operation() { Name = "Долг" }
		};

		return operations;
	}
	private List<Transaction> CreateTestTransactions(List<Operation> operations)
	{
		List<Transaction> transaction = new List<Transaction>()
		{
			new Transaction() { Sum = -2000000, DateTime = DateTime.Now, OperationId = operations[0].Id },
			new Transaction() { Sum = -1000000, DateTime = DateTime.Now, OperationId = operations[1].Id, Discription = "English" },
			new Transaction() { Sum = -500000, DateTime = DateTime.Now, OperationId = operations[1].Id, Discription = "Math" },
			new Transaction() { Sum = 2000000, DateTime = new DateTime(2012, 12, 12), OperationId = operations[2].Id },
			new Transaction() { Sum = 2000000, DateTime = new DateTime(2012, 12, 12), OperationId = operations[2].Id }
		};

		return transaction;
	}
}

