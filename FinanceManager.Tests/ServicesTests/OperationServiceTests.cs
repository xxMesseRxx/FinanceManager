namespace FinanceManager.Tests.ServicesTests;

using FinanceManager.Model;
using FinanceManager.Services;
using FinanceManager.DAL.DTO.Operation;

public class OperationServiceTests
{
	[Fact]
	public void AddOperationAsync_NameIsUnique_5OperationsExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			OperationService operationService = servicesGreator.GetOperationService();
			OperationCreateDto operationCreateDto = new OperationCreateDto() { Name = "��������" };
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
	public void AddOperationAsync_NameIsNotUnique_AggregateExceptionExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			OperationService operationService = servicesGreator.GetOperationService();
			OperationCreateDto operationCreateDto = new OperationCreateDto() { Name = "��������" };

            //Act
            Assert.Throws<AggregateException>(() => operationService.AddAsync(operationCreateDto).Wait());
		}
		finally
		{
			dbCreator.Dispose();
		}
	}
    [Fact]
    public void AddOperationAsync_NullArg_AggregateExceptionExpected()
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
    public void AddOperationAsync_EmptyName_AggregateExceptionExpected()
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
	public void EditOperationAsync_CorArgs_ChangedOperationExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			OperationService operationService = servicesGreator.GetOperationService();
			List<Operation> operations = operationService.GetAllAsync().Result;
            string expectedNewName = "�����";
			OperationUpdateDto operationUpdateDto = new OperationUpdateDto()
			{
				Id = operations[0].Id,
				Name = expectedNewName
			};

			//Act
			operationService.EditAsync(operationUpdateDto).Wait();
			string result = operationService.GetAsync(operations[0].Id).Result.Name;

			//Assert
			Assert.Equal(expectedNewName, result);
		}
		finally
		{
			dbCreator.Dispose();
		}
	}
	[Fact]
	public void EditOperationAsync_NameIsNotUnique_AggregateExceptionExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			OperationService operationService = servicesGreator.GetOperationService();
			List<Operation> operations = operationService.GetAllAsync().Result;
            OperationUpdateDto operationUpdateDto = new OperationUpdateDto()
            {
                Id = operations[1].Id,
                Name = operations[0].Name
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
    public void EditOperationAsync_NameIsEmpty_AggregateExceptionExpected()
    {
        var dbCreator = new TestDBCreator();

        try
        {
            //Arrange
            dbCreator.CreateTestDB();
            ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
            OperationService operationService = servicesGreator.GetOperationService();
            List<Operation> operations = operationService.GetAllAsync().Result;
            OperationUpdateDto operationUpdateDto = new OperationUpdateDto()
            {
                Id = operations[1].Id,
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
	public void EditOperationAsync_IdIsNotExist_AggregateExceptionExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			OperationService operationService = servicesGreator.GetOperationService();
			List<Operation> operations = operationService.GetAllAsync().Result;
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
	public void GetOperationAsync_CorId_RequiredOperationExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			OperationService operationService = servicesGreator.GetOperationService();
			List<Operation> operations = operationService.GetAllAsync().Result;
			int expectedOperationId = operations[0].Id;

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
    public void GetOperationAsync_CorName_RequiredOperationExpected()
    {
        var dbCreator = new TestDBCreator();

        try
        {
            //Arrange
            dbCreator.CreateTestDB();
            ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
            OperationService operationService = servicesGreator.GetOperationService();
            List<Operation> operations = operationService.GetAllAsync().Result;
            string expectedOperationName = operations[0].Name;

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
	public void GetOperationAsync_IncorId_NullExpected()
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
    public void GetOperationAsync_IncorName_NullExpected()
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
	public void RemoveOperationAsync_OperationWithoutTransaction_3OperationsExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			OperationService operationService = servicesGreator.GetOperationService();
			List<Operation> operations = operationService.GetAllAsync().Result;
			int expectedOperationCount = 3;

			//Act
			operationService.RemoveAsync(operations[0].Id).Wait();
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
	public void RemoveOperationAsync_OperationWithTransaction_AggregateExceptionExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
			OperationService operationService = servicesGreator.GetOperationService();
			List<Operation> operations = operationService.GetAllAsync().Result;

			//Act
			Assert.Throws<AggregateException>(() => operationService
														.RemoveAsync(operations[1].Id)
														.Wait());
		}
		finally
		{
			dbCreator.Dispose();
		}
	}
	[Fact]
	public void RemoveOperationAsync_IncorId_AggregateExceptionExpected()
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