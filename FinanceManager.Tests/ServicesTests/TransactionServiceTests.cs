namespace FinanceManager.Tests.ServicesTests;

using FinanceManager.DAL.DTO.Operation;
using FinanceManager.DAL.DTO.Transaction;
using FinanceManager.Model;
using FinanceManager.Services;

public class TransactionServiceTests
{
	[Fact]
	public void AddAsync_CorArg_6TransactionsExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			TransactionService transactionService = servicesGreator.GetTransactionService();
			OperationService operationService = servicesGreator.GetOperationService();
			List<OperationViewModel> operationsViewModel = operationService.GetAllAsync().Result;
			TransactionCreateDto transactionCreateDto = new TransactionCreateDto()
			{
				Sum = 500,
				Discription = "Some discription",
				DateTime = DateTime.Now,
				OperationId = operationsViewModel[0].Id
			};
			int expectedTransactionCount = 6;

			//Act
			transactionService.AddAsync(transactionCreateDto).Wait();
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
	public void AddAsync_OperationIdIsNotExist_AggregateExceptionExpected()
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
														.AddAsync(transactionCreateDto)
														.Wait());
		}
		finally
		{
			dbCreator.Dispose();
		}
	}

	[Fact]
	public void EditAsync_CorArg_ChangedTransactionExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			TransactionService transactionService = servicesGreator.GetTransactionService();
			OperationService operationService = servicesGreator.GetOperationService();
			List<OperationViewModel> operationsViewModel = operationService.GetAllAsync().Result;
			List<TransactionViewModel> transactionsViewModel = transactionService.GetAllAsync().Result;

			TransactionUpdateDto transactionUpdateDto = new TransactionUpdateDto()
			{
				Id = transactionsViewModel[0].Id,
				Sum = 50001,
				DateTime = DateTime.Now,
				Discription = "New",
				OperationId = operationsViewModel[3].Id
			};

			//Act
			transactionService.EditAsync(transactionUpdateDto).Wait();
			TransactionViewModel result = transactionService.GetAsync(transactionUpdateDto.Id).Result;

			//Assert
			Assert.Equal(transactionUpdateDto.Discription, result.Discription);
		}
		finally
		{
			dbCreator.Dispose();
		}
	}
	[Fact]
	public void EditAsync_TransactionIdIsNotExist_AggregateExceptionExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			TransactionService transactionService = servicesGreator.GetTransactionService();
			List<TransactionViewModel> transactionsViewModel = transactionService.GetAllAsync().Result;

            TransactionUpdateDto transactionUpdateDto = new TransactionUpdateDto()
            {
                Id = -6,
                Sum = 50001,
                DateTime = DateTime.Now,
                Discription = "New",
                OperationId = transactionsViewModel[0].OperationId
            };

            //Act
            Assert.Throws<AggregateException>(() => transactionService
														.EditAsync(transactionUpdateDto)
														.Wait());
		}
		finally
		{
			dbCreator.Dispose();
		}
	}
	[Fact]
	public void EditAsync_OperationIdIsNotExist_AggregateExceptionExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			TransactionService transactionService = servicesGreator.GetTransactionService();
			List<TransactionViewModel> transactionsViewModel = transactionService.GetAllAsync().Result;

            TransactionUpdateDto transactionUpdateDto = new TransactionUpdateDto()
            {
                Id = transactionsViewModel[0].Id,
                Sum = 50001,
                DateTime = DateTime.Now,
                Discription = "New",
                OperationId = -6
            };

            //Act
            Assert.Throws<AggregateException>(() => transactionService
														.EditAsync(transactionUpdateDto)
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
	public void GetAsync_CorId_RequiredTransactionExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			TransactionService transactionService = servicesGreator.GetTransactionService();
			List<TransactionViewModel> transactionsViewModel = transactionService.GetAllAsync().Result;
			var expectedTransaction = transactionsViewModel[2];

			//Act
			var result = transactionService.GetAsync(expectedTransaction.Id).Result;

			//Assert
			Assert.Equal(expectedTransaction, result);
		}
		finally
		{
			dbCreator.Dispose();
		}
	}
	[Fact]
	public void GetAsync_IncorId_NullExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			TransactionService transactionService = servicesGreator.GetTransactionService();

			//Act
			var result = transactionService.GetAsync(-5).Result;

			//Assert
			Assert.Null(result);
		}
		finally
		{
			dbCreator.Dispose();
		}
	}

    [Fact]
    public void GetByDateAsync_CorDate_2TransactionsExpected()
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
            int result = transactionService.GetByDateAsync(date).Result.Count;

			//Assert
			Assert.Equal(expected, result);
        }
        finally
        {
            dbCreator.Dispose();
        }
    }
    [Fact]
    public void GetByDateAsync_IncorDate_EmptyListExpected()
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
            int result = transactionService.GetByDateAsync(date).Result.Count;

            //Assert
            Assert.Equal(expected, result);
        }
        finally
        {
            dbCreator.Dispose();
        }
    }
    [Fact]
    public void GetByDateAsync_CorDates_3TransactionsExpected()
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
							.GetByDateAsync(startDate, endDate)
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
    public void GetByDateAsync_IncorDates_EmptyListExpected()
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
                            .GetByDateAsync(startDate, endDate)
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
	public void GetWithOperIdAsync_CorId_1TransactionExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			TransactionService transactionService = servicesGreator.GetTransactionService();
			OperationService operationService = servicesGreator.GetOperationService();
			List<OperationViewModel> operationsViewModel = operationService.GetAllAsync().Result;
            OperationViewModel operationViewModel = operationsViewModel.First(o => o.Name == "Оплата квартиры");
			int expected = 1;

			//Act
			int result = transactionService.GetWithOperIdAsync(operationViewModel.Id).Result.Count;

			//Assert
			Assert.Equal(expected, result);
		}
		finally
		{
			dbCreator.Dispose();
		}
	}
	[Fact]
	public void GetWithOperIdAsync_IncorId_EmptyListExpected()
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
			int result = transactionService.GetWithOperIdAsync(-5).Result.Count;

			//Assert
			Assert.Equal(expected, result);
		}
		finally
		{
			dbCreator.Dispose();
		}
	}

	[Fact]
	public void RemoveAsync_CorId_4TransactionsExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			TransactionService transactionService = servicesGreator.GetTransactionService();
			List<TransactionViewModel> transactionsViewModel = transactionService.GetAllAsync().Result;
			int expected = 4;

			//Act
			transactionService.RemoveAsync(transactionsViewModel[0].Id).Wait();
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
	public void RemoveAsync_IncorId_AggregateExceptionExpected()
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
														.RemoveAsync(-6)
														.Wait());
		}
		finally
		{
			dbCreator.Dispose();
		}
	}
}
