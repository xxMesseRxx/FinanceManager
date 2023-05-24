namespace FinanceManager.Tests.ServicesTests;

using FinanceManager.Model;
using FinanceManager.Services;
using FinanceManager.DAL.DTO.Operation;

public class OperationServiceTests
{
	[Fact]
	public void AddAsync_NameIsUnique_5OperationsExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			OperationService operationService = servicesGreator.GetOperationService();
			OperationCreateDto operationCreateDto = new OperationCreateDto() { Name = "Продукты" };
			int expectedOperationCount = 5;

			//Act
			operationService.AddAsync(operationCreateDto).Wait();
			int result = operationService.GetAllAsync().Result.Count;

			//Assert
			Assert.Equal(expectedOperationCount, result);
		}
		finally
		{
			dbCreator.Dispose();
		}
	}
	[Fact]
	public void AddAsync_NameIsNotUnique_AggregateExceptionExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			OperationService operationService = servicesGreator.GetOperationService();
			OperationCreateDto operationCreateDto = new OperationCreateDto() { Name = "Зарплата" };

            //Act
            Assert.Throws<AggregateException>(() => operationService.AddAsync(operationCreateDto).Wait());
		}
		finally
		{
			dbCreator.Dispose();
		}
	}
    [Fact]
    public void AddAsync_NullArg_AggregateExceptionExpected()
    {
        var dbCreator = new TestDBCreator();

        try
        {
            //Arrange
            dbCreator.CreateTestDB();
            ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
            OperationService operationService = servicesGreator.GetOperationService();

            //Act
            Assert.Throws<AggregateException>(() => operationService.AddAsync(null).Wait());
        }
        finally
        {
            dbCreator.Dispose();
        }
    }
    [Fact]
    public void AddAsync_EmptyName_AggregateExceptionExpected()
    {
        var dbCreator = new TestDBCreator();

        try
        {
            //Arrange
            dbCreator.CreateTestDB();
            ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
            OperationService operationService = servicesGreator.GetOperationService();
			OperationCreateDto operationCreateDto = new OperationCreateDto() { Name = "" };

            //Act
            Assert.Throws<AggregateException>(() => operationService.AddAsync(operationCreateDto).Wait());
        }
        finally
        {
            dbCreator.Dispose();
        }
    }

    [Fact]
	public void EditAsync_CorArgs_ChangedOperationExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			OperationService operationService = servicesGreator.GetOperationService();
			List<OperationViewModel> operationsViewModel = operationService.GetAllAsync().Result;
            string expectedNewName = "Школа";
			OperationUpdateDto operationUpdateDto = new OperationUpdateDto()
			{
				Id = operationsViewModel[0].Id,
				Name = expectedNewName
			};

			//Act
			operationService.EditAsync(operationUpdateDto).Wait();
			string result = operationService.GetAsync(operationsViewModel[0].Id).Result.Name;

			//Assert
			Assert.Equal(expectedNewName, result);
		}
		finally
		{
			dbCreator.Dispose();
		}
	}
	[Fact]
	public void EditAsync_NameIsNotUnique_AggregateExceptionExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			OperationService operationService = servicesGreator.GetOperationService();
			List<OperationViewModel> operationsViewModel = operationService.GetAllAsync().Result;
            OperationUpdateDto operationUpdateDto = new OperationUpdateDto()
            {
                Id = operationsViewModel[1].Id,
                Name = operationsViewModel[0].Name
            };

            //Act
            Assert.Throws<AggregateException>(() => operationService
														.EditAsync(operationUpdateDto)
														.Wait());
		}
		finally
		{
			dbCreator.Dispose();
		}
	}
    [Fact]
    public void EditAsync_NameIsEmpty_AggregateExceptionExpected()
    {
        var dbCreator = new TestDBCreator();

        try
        {
            //Arrange
            dbCreator.CreateTestDB();
            ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
            OperationService operationService = servicesGreator.GetOperationService();
            List<OperationViewModel> operationsViewModel = operationService.GetAllAsync().Result;
            OperationUpdateDto operationUpdateDto = new OperationUpdateDto()
            {
                Id = operationsViewModel[1].Id,
                Name = ""
            };

            //Act
            Assert.Throws<AggregateException>(() => operationService
                                                        .EditAsync(operationUpdateDto)
                                                        .Wait());
        }
        finally
        {
            dbCreator.Dispose();
        }
    }
    [Fact]
	public void EditAsync_IdIsNotExist_AggregateExceptionExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			OperationService operationService = servicesGreator.GetOperationService();
            OperationUpdateDto operationUpdateDto = new OperationUpdateDto()
            {
                Id = -6,
                Name = "New Name"
            };

            //Act
            Assert.Throws<AggregateException>(() => operationService
														.EditAsync(operationUpdateDto)
														.Wait());
		}
		finally
		{
			dbCreator.Dispose();
		}
	}

	[Fact]
	public void GetAllAsync_Get_4OperationsExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			OperationService operationService = servicesGreator.GetOperationService();
			int expectedOperationCount = 4;

			//Act
			int result = operationService.GetAllAsync().Result.Count;

			//Assert
			Assert.Equal(expectedOperationCount, result);
		}
		finally
		{
			dbCreator.Dispose();
		}
	}

	[Fact]
	public void GetAsync_CorId_RequiredOperationExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			OperationService operationService = servicesGreator.GetOperationService();
			List<OperationViewModel> operationsViewModel = operationService.GetAllAsync().Result;
			int expectedOperationId = operationsViewModel[0].Id;

			//Act
			var requiredOperation = operationService.GetAsync(expectedOperationId).Result;
			int result = requiredOperation.Id;

			//Assert
			Assert.Equal(expectedOperationId, result);
		}
		finally
		{
			dbCreator.Dispose();
		}
	}
    [Fact]
    public void GetAsync_CorName_RequiredOperationExpected()
    {
        var dbCreator = new TestDBCreator();

        try
        {
            //Arrange
            dbCreator.CreateTestDB();
            ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
            OperationService operationService = servicesGreator.GetOperationService();
            List<OperationViewModel> operationsViewModel = operationService.GetAllAsync().Result;
            string expectedOperationName = operationsViewModel[0].Name;

            //Act
            var requiredOperation = operationService.GetAsync(expectedOperationName).Result;
            string result = requiredOperation.Name;

            //Assert
            Assert.Equal(expectedOperationName, result);
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
			OperationService operationService = servicesGreator.GetOperationService();

			//Act
			var result = operationService.GetAsync(-7).Result;

			//Assert
			Assert.Null(result);
		}
		finally
		{
			dbCreator.Dispose();
		}
	}
    [Fact]
    public void GetAsync_IncorName_NullExpected()
    {
        var dbCreator = new TestDBCreator();

        try
        {
            //Arrange
            dbCreator.CreateTestDB();
            ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
            OperationService operationService = servicesGreator.GetOperationService();

            //Act
            var result = operationService.GetAsync("blabla").Result;

            //Assert
            Assert.Null(result);
        }
        finally
        {
            dbCreator.Dispose();
        }
    }

    [Fact]
	public void RemoveAsync_OperationWithoutTransaction_3OperationsExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			OperationService operationService = servicesGreator.GetOperationService();
			List<OperationViewModel> operationsViewModel = operationService.GetAllAsync().Result;
			int expectedOperationCount = 3;

			//Act
			operationService.RemoveAsync(operationsViewModel[0].Id).Wait();
			int result = operationService.GetAllAsync().Result.Count;

			//Assert
			Assert.Equal(expectedOperationCount, result);
		}
		finally
		{
			dbCreator.Dispose();
		}
	}
	[Fact]
	public void RemoveAsync_OperationWithTransaction_AggregateExceptionExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			OperationService operationService = servicesGreator.GetOperationService();
			List<OperationViewModel> operationsViewModel = operationService.GetAllAsync().Result;

			//Act
			Assert.Throws<AggregateException>(() => operationService
														.RemoveAsync(operationsViewModel[1].Id)
														.Wait());
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
			OperationService operationService = servicesGreator.GetOperationService();

			//Act
			Assert.Throws<AggregateException>(() => operationService
														.RemoveAsync(-3)
														.Wait());
		}
		finally
		{
			dbCreator.Dispose();
		}
	}
}