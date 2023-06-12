namespace FinManagerWebClient.Library.Requests;

using FinManagerWebClient.DTO.Reports;

public interface IReportsService
{
    public Task<DailyReport> GetDailyReportAsync(DateTime date);
    public Task<PeriodReport> GetPeriodReportAsync(DateTime startDate, DateTime endDate);
}
