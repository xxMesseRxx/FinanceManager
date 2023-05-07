namespace FinanceManager.Tests.ServicesTests;

using FinanceManager.Model;
using FinanceManager.Services;

public class TransactionServiceTests
{
	[Fact]
	public void AddTransactionAsync_CorArgs_6TransactionExpected()
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
			OperationService operationService = servicesGreator.GetOperationService();

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
			List<Transaction> transaction = transactionService.GetAllAsync().Result;

			Transaction editedTransaction = transaction[0];
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
}
