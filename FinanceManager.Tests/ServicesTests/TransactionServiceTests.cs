namespace FinanceManager.Tests.ServicesTests;

using FinanceManager.DAL.DTO.Transaction;
using FinanceManager.Model;
using FinanceManager.Services;

public class TransactionServiceTests
{
	[Fact]
	public void AddTransactionAsync_CorArg_6TransactionsExpected()
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
			TransactionCreateDto transactionCreateDto = new TransactionCreateDto()
			{
				Sum = 500,
				Discription = "Some discription",
				DateTime = DateTime.Now,
				OperationId = operations[0].Id
			};
			int expectedTransactionCount = 6;

			//Act
			transactionService.AddTransactionAsync(transactionCreateDto).Wait();
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
	public void AddTransactionAsync_OperationIdIsNotExist_AggregateExceptionExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			TransactionService transactionService = servicesGreator.GetTransactionService();
            TransactionCreateDto transactionCreateDto = new TransactionCreateDto()
            {
                Sum = 500,
                Discription = "Some discription",
                DateTime = DateTime.Now,
                OperationId = -5
            };

            //Act
            Assert.Throws<AggregateException>(() => transactionService
														.AddTransactionAsync(transactionCreateDto)
														.Wait());
		}
		finally
		{
			dbCreator.Dispose();
		}
	}

	[Fact]
	public void EditTransactionAsync_CorArg_ChangedTransactionExpected()
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

			TransactionUpdateDto transactionUpdateDto = new TransactionUpdateDto()
			{
				Id = transactions[0].Id,
				Sum = 50001,
				DateTime = DateTime.Now,
				Discription = "New",
				OperationId = operations[3].Id
			};

			//Act
			transactionService.EditTransactionAsync(transactionUpdateDto).Wait();
			Transaction result = transactionService.GetTransactionAsync(transactionUpdateDto.Id).Result;

			//Assert
			Assert.Equal(transactionUpdateDto.Discription, result.Discription);
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

            TransactionUpdateDto transactionUpdateDto = new TransactionUpdateDto()
            {
                Id = -6,
                Sum = 50001,
                DateTime = DateTime.Now,
                Discription = "New",
                OperationId = transactions[0].OperationId
            };

            //Act
            Assert.Throws<AggregateException>(() => transactionService
														.EditTransactionAsync(transactionUpdateDto)
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

            TransactionUpdateDto transactionUpdateDto = new TransactionUpdateDto()
            {
                Id = transactions[0].Id,
                Sum = 50001,
                DateTime = DateTime.Now,
                Discription = "New",
                OperationId = -6
            };

            //Act
            Assert.Throws<AggregateException>(() => transactionService
														.EditTransactionAsync(transactionUpdateDto)
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
    public void GetTransactionsByDateAsync_CorDate_2TransactionsExpected()
    {
        var dbCreator = new TestDBCreator();

        try
        {
            //Arrange
            dbCreator.CreateTestDB();
            ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
            TransactionService transactionService = servicesGreator.GetTransactionService();
			DateOnly date = new DateOnly(2012, 12, 12);
			int expected = 2;

			//Act
            int result = transactionService.GetTransactionsByDateAsync(date).Result.Count;

			//Assert
			Assert.Equal(expected, result);
        }
        finally
        {
            dbCreator.Dispose();
        }
    }
    [Fact]
    public void GetTransactionsByDateAsync_IncorDate_EmptyListExpected()
    {
        var dbCreator = new TestDBCreator();

        try
        {
            //Arrange
            dbCreator.CreateTestDB();
            ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
            TransactionService transactionService = servicesGreator.GetTransactionService();
            DateOnly date = new DateOnly(1900, 12, 12);
            int expected = 0;

            //Act
            int result = transactionService.GetTransactionsByDateAsync(date).Result.Count;

            //Assert
            Assert.Equal(expected, result);
        }
        finally
        {
            dbCreator.Dispose();
        }
    }
    [Fact]
    public void GetTransactionsByDateAsync_CorDates_3TransactionsExpected()
    {
        var dbCreator = new TestDBCreator();

        try
        {
            //Arrange
            dbCreator.CreateTestDB();
            ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
            TransactionService transactionService = servicesGreator.GetTransactionService();
            DateOnly startDate = new DateOnly(2016, 01, 08);
            DateOnly endDate = new DateOnly(2020, 12, 10);
            int expected = 3;

            //Act
            int result = transactionService
							.GetTransactionsByDateAsync(startDate, endDate)
							.Result
							.Count;

            //Assert
            Assert.Equal(expected, result);
        }
        finally
        {
            dbCreator.Dispose();
        }
    }
    [Fact]
    public void GetTransactionsByDateAsync_IncorDates_EmptyListExpected()
    {
        var dbCreator = new TestDBCreator();

        try
        {
            //Arrange
            dbCreator.CreateTestDB();
            ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
            TransactionService transactionService = servicesGreator.GetTransactionService();
            DateOnly startDate = new DateOnly(2002, 01, 08);
            DateOnly endDate = new DateOnly(2010, 12, 10);
            int expected = 0;

            //Act
            int result = transactionService
                            .GetTransactionsByDateAsync(startDate, endDate)
                            .Result
                            .Count;

            //Assert
            Assert.Equal(expected, result);
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
