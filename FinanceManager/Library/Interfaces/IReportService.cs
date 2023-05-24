namespace FinanceManager.Library.Interfaces;

using FinanceManager.DAL.DTO.Reports;

public interface IReportService
{
    public Task<DailyReport> GetDailyReportAsync(DateOnly date);
    public Task<PeriodReport> GetPeriodReportAsync(DateOnly startDate, DateOnly endDate);
}
