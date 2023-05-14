namespace FinanceManager.Library.Interfaces;

using FinanceManager.Model;

public interface IReportService
{
    public Task<DailyReport> GetDailyReportAsync(DateOnly date);
}
