namespace FinanceManager.Library.Interfaces;

using FinanceManager.DAL;

public interface IReportService
{
    public Task<DailyReport> GetDailyReportAsync(DateOnly date);
    public Task<PeriodReport> GetPeriodReportAsync(DateOnly startDate, DateOnly endDate);
}
