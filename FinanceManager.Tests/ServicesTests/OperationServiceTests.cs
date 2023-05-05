namespace FinanceManager.Tests.ServicesTests;

using FinanceManager.Model;
using FinanceManager.Services;
using Microsoft.EntityFrameworkCore;

public class OperationServiceTests
{
	[Fact]
	public void AddOperationAsync_TypeIsUnique_5OperationExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator();
			OperationService operationService = servicesGreator.GetOperationService();
			int expectedOperationCount = 5;

			//Act
			operationService.AddOperationAsync("Продукты").Wait();
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
	public void AddOperationAsync_TypeIsNotUnique_AggregateExceptionExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator();
			OperationService operationService = servicesGreator.GetOperationService();

			//Act
			Assert.Throws<AggregateException>(() => operationService.AddOperationAsync("Зарплата").Wait());
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
			ServicesGreator servicesGreator = new ServicesGreator();
			OperationService operationService = servicesGreator.GetOperationService();
			List<Operation> operations = operationService.GetAllAsync().Result;
			string expectedNewType = "Школа";

			//Act
			operationService.EditOperationAsync(operations[0].Id, expectedNewType).Wait();
			string result = operationService.GetOperationAsync(operations[0].Id).Result.Type;

			//Assert
			Assert.Equal(expectedNewType, result);
		}
		finally
		{
			dbCreator.Dispose();
		}
	}
	[Fact]
	public void EditOperationAsync_TypeIsNotUnique_AggregateExceptionExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator();
			OperationService operationService = servicesGreator.GetOperationService();
			List<Operation> operations = operationService.GetAllAsync().Result;

			//Act
			Assert.Throws<AggregateException>(() => operationService
														.EditOperationAsync(operations[1].Id, operations[0].Type)
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
			ServicesGreator servicesGreator = new ServicesGreator();
			OperationService operationService = servicesGreator.GetOperationService();
			List<Operation> operations = operationService.GetAllAsync().Result;

			//Act
			Assert.Throws<AggregateException>(() => operationService
														.EditOperationAsync(-6, "New Type")
														.Wait());
		}
		finally
		{
			dbCreator.Dispose();
		}
	}

	[Fact]
	public void GetAllAsync_Get_4OperationExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator();
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
			ServicesGreator servicesGreator = new ServicesGreator();
			OperationService operationService = servicesGreator.GetOperationService();
			List<Operation> operations = operationService.GetAllAsync().Result;
			int expectedOperationId = operations[0].Id;

			//Act
			var requiredOperation = operationService.GetOperationAsync(expectedOperationId).Result;
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
	public void GetOperationAsync_IncorId_NullExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator();
			OperationService operationService = servicesGreator.GetOperationService();

			//Act
			var result = operationService.GetOperationAsync(-7).Result;

			//Assert
			Assert.Null(result);
		}
		finally
		{
			dbCreator.Dispose();
		}
	}

	[Fact]
	public void RemoveOperationAsync_OperationWithoutFinOper_3OperationExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator();
			OperationService operationService = servicesGreator.GetOperationService();
			List<Operation> operations = operationService.GetAllAsync().Result;
			Operation operationWithoutFinOper = operations.Find(o => o.FinancialOperations == null);
			int expectedOperationCount = 3;

			//Act
			operationService.RemoveOperationAsync(operationWithoutFinOper.Id).Wait();
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
	public void RemoveOperationAsync_OperationWithFinOper_AggregateExceptionExpected()
	{
		var dbCreator = new TestDBCreator();

		try
		{
			//Arrange
			dbCreator.CreateTestDB();
			ServicesGreator servicesGreator = new ServicesGreator();
			OperationService operationService = servicesGreator.GetOperationService();
			List<Operation> operations = operationService.GetAllAsync().Result;

			//Act
			Assert.Throws<AggregateException>(() => operationService
														.RemoveOperationAsync(operations[1].Id)
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
			ServicesGreator servicesGreator = new ServicesGreator();
			OperationService operationService = servicesGreator.GetOperationService();
			List<Operation> operations = operationService.GetAllAsync().Result;

			//Act
			Assert.Throws<AggregateException>(() => operationService
														.RemoveOperationAsync(-3)
														.Wait());
		}
		finally
		{
			dbCreator.Dispose();
		}
	}
}