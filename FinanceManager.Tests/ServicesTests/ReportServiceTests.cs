namespace FinanceManager.Tests.ServicesTests;

using FinanceManager.Library;
using FinanceManager.Model;
using FinanceManager.Services;

public class ReportServiceTests
{
    [Fact]
    public void GetDailyReportAsync_DateWith2Transactions_DailyReportWith2TransactionsExpected()
    {
        var dbCreator = new TestDBCreator();

        try
        {
            //Arrange
            dbCreator.CreateTestDB();
            ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
            ReportService reportService = servicesGreator.GetReportService();
            DateOnly date = new DateOnly(2012, 12, 12);
            int expectedTransactionsCount = 2;

            //Act
            Report report = reportService.GetDailyReportAsync(date).Result;
            int actual = report.Transactions.Count;

            //Assert
            Assert.Equal(expectedTransactionsCount, actual);
        }
        finally
        {
            dbCreator.Dispose();
        }
    }
    [Fact]
    public void GetDailyReportAsync_DateWithoutTransactions_DailyReportWith0TransactionsExpected()
    {
        var dbCreator = new TestDBCreator();

        try
        {
            //Arrange
            dbCreator.CreateTestDB();
            ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
            ReportService reportService = servicesGreator.GetReportService();
            DateOnly date = new DateOnly(1960, 12, 12);
            int expectedTransactionsCount = 0;

            //Act
            Report report = reportService.GetDailyReportAsync(date).Result;
            int actual = report.Transactions.Count;

            //Assert
            Assert.Equal(expectedTransactionsCount, actual);
        }
        finally
        {
            dbCreator.Dispose();
        }
    }
    [Fact]
    public void GetPeriodReportAsync_PeriodWith3Transactions_DailyReportWith3TransactionsExpected()
    {
        var dbCreator = new TestDBCreator();

        try
        {
            //Arrange
            dbCreator.CreateTestDB();
            ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
            ReportService reportService = servicesGreator.GetReportService();
            DateOnly startDate = new DateOnly(2015, 12, 12);
            DateOnly endDate = new DateOnly(2020, 12, 12);
            int expectedTransactionsCount = 3;

            //Act
            Report report = reportService.GetPeriodReportAsync(startDate, endDate).Result;
            int actual = report.Transactions.Count;

            //Assert
            Assert.Equal(expectedTransactionsCount, actual);
        }
        finally
        {
            dbCreator.Dispose();
        }
    }
    [Fact]
    public void GetPeriodReportAsync_PeriodWithoutTransactions_DailyReportWith3TransactionsExpected()
    {
        var dbCreator = new TestDBCreator();

        try
        {
            //Arrange
            dbCreator.CreateTestDB();
            ServicesGreator servicesGreator = new ServicesGreator(dbCreator.DbName);
            ReportService reportService = servicesGreator.GetReportService();
            DateOnly startDate = new DateOnly(2008, 12, 12);
            DateOnly endDate = new DateOnly(2010, 12, 12);
            int expectedTransactionsCount = 0;

            //Act
            Report report = reportService.GetPeriodReportAsync(startDate, endDate).Result;
            int actual = report.Transactions.Count;

            //Assert
            Assert.Equal(expectedTransactionsCount, actual);
        }
        finally
        {
            dbCreator.Dispose();
        }
    }
}
