namespace FinanceManager.Tests.ServicesTests;

using FinanceManager.Model;
using FinanceManager.Services;

public class TransactionServiceTests
{
	[Fact]
	public void AddTransactionAsync_CorArgs_6TransactionsExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			TransactionService transactionService = servicesGreator.GetTransactionService();
			OperationService operationService = servicesGreator.GetOperationService();
			List<Operation> operations = operationService.GetAllAsync().Result;
			int expectedTransactionCount = 6;

			//Act
			transactionService.AddTransactionAsync(500, "sdf", DateTime.Now, operations[0].Id).Wait();
			int result = transactionService.GetAllAsync().Result.Count;

			//Assert
			Assert.Equal(expectedTransactionCount, result);
		}
		finally
		{
			dbCreator.Dispose();
		}
	}
	[Fact]
	public void AddTransactionAsync_OperationIsNotExist_AggregateExceptionExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			TransactionService transactionService = servicesGreator.GetTransactionService();

			//Act
			Assert.Throws<AggregateException>(() => transactionService
														.AddTransactionAsync(500, "sdf", DateTime.Now, -5)
														.Wait());
		}
		finally
		{
			dbCreator.Dispose();
		}
	}

	[Fact]
	public void EditTransactionAsync_CorArgs_ChangedTransactionExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			TransactionService transactionService = servicesGreator.GetTransactionService();
			OperationService operationService = servicesGreator.GetOperationService();
			List<Operation> operations = operationService.GetAllAsync().Result;
			List<Transaction> transactions = transactionService.GetAllAsync().Result;

			Transaction editedTransaction = transactions[0];
			editedTransaction.Sum = 50001;
			editedTransaction.DateTime = DateTime.Now;
			editedTransaction.Discription = "New";
			editedTransaction.OperationId = operations[3].Id;

			//Act
			transactionService.EditTransactionAsync(editedTransaction.Id,
													editedTransaction.Sum,
													editedTransaction.Discription,
													editedTransaction.DateTime,
													editedTransaction.OperationId).Wait();
			Transaction result = transactionService.GetTransactionAsync(editedTransaction.Id).Result;

			//Assert
			Assert.Equal(editedTransaction, result);
		}
		finally
		{
			dbCreator.Dispose();
		}
	}
	[Fact]
	public void EditTransactionAsync_TransactionIdIsNotExist_AggregateExceptionExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			TransactionService transactionService = servicesGreator.GetTransactionService();
			List<Transaction> transactions = transactionService.GetAllAsync().Result;

			//Act
			Assert.Throws<AggregateException>(() => transactionService
														.EditTransactionAsync(-6, 500, "sdf", DateTime.Now, transactions[0].OperationId)
														.Wait());
		}
		finally
		{
			dbCreator.Dispose();
		}
	}
	[Fact]
	public void EditTransactionAsync_OperationIdIsNotExist_AggregateExceptionExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			TransactionService transactionService = servicesGreator.GetTransactionService();
			List<Transaction> transactions = transactionService.GetAllAsync().Result;

			//Act
			Assert.Throws<AggregateException>(() => transactionService
														.EditTransactionAsync(transactions[0].Id, 500, "sdf", DateTime.Now, -6)
														.Wait());
		}
		finally
		{
			dbCreator.Dispose();
		}
	}

	[Fact]
	public void GetAllAsync_Get_5TransactionsExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			TransactionService transactionService = servicesGreator.GetTransactionService();
			int expectedTransactionCount = 5;

			//Act
			int result = transactionService.GetAllAsync().Result.Count;

			//Assert
			Assert.Equal(expectedTransactionCount, result);
		}
		finally
		{
			dbCreator.Dispose();
		}
	}

	[Fact]
	public void GetTransactionAsync_CorId_RequiredTransactionExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			TransactionService transactionService = servicesGreator.GetTransactionService();
			List<Transaction> transactions = transactionService.GetAllAsync().Result;
			var expectedTransaction = transactions[2];

			//Act
			var result = transactionService.GetTransactionAsync(expectedTransaction.Id).Result;

			//Assert
			Assert.Equal(expectedTransaction, result);
		}
		finally
		{
			dbCreator.Dispose();
		}
	}
	[Fact]
	public void GetTransactionAsync_IncorId_NullExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			TransactionService transactionService = servicesGreator.GetTransactionService();

			//Act
			var result = transactionService.GetTransactionAsync(-5).Result;

			//Assert
			Assert.Null(result);
		}
		finally
		{
			dbCreator.Dispose();
		}
	}

	[Fact]
	public void GetTransactionWithOperIdAsync_CorId_1TransactionExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			TransactionService transactionService = servicesGreator.GetTransactionService();
			OperationService operationService = servicesGreator.GetOperationService();
			List<Operation> operations = operationService.GetAllAsync().Result;
			Operation operation = operations.First(o => o.Name == "Оплата квартиры");
			int expected = 1;

			//Act
			int result = transactionService.GetTransactionWithOperIdAsync(operation.Id).Result.Count;

			//Assert
			Assert.Equal(expected, result);
		}
		finally
		{
			dbCreator.Dispose();
		}
	}
	[Fact]
	public void GetTransactionWithOperIdAsync_IncorId_EmptyListExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			TransactionService transactionService = servicesGreator.GetTransactionService();
			int expected = 0;

			//Act
			int result = transactionService.GetTransactionWithOperIdAsync(-5).Result.Count;

			//Assert
			Assert.Equal(expected, result);
		}
		finally
		{
			dbCreator.Dispose();
		}
	}

	[Fact]
	public void RemoveTransactionAsync_CorId_4TransactionsExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			TransactionService transactionService = servicesGreator.GetTransactionService();
			List<Transaction> transactions = transactionService.GetAllAsync().Result;
			int expected = 4;

			//Act
			transactionService.RemoveTransactionAsync(transactions[0].Id).Wait();
			int result = transactionService.GetAllAsync().Result.Count;

			//Assert
			Assert.Equal(expected, result);
		}
		finally
		{
			dbCreator.Dispose();
		}
	}
	[Fact]
	public void RemoveTransactionAsync_IncorId_AggregateExceptionExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			TransactionService transactionService = servicesGreator.GetTransactionService();

			//Act
			Assert.Throws<AggregateException>(() => transactionService
														.RemoveTransactionAsync(-6)
														.Wait());
		}
		finally
		{
			dbCreator.Dispose();
		}
	}
}
