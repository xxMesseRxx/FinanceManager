namespace FinanceManager.Services;

using FinanceManager.DAL;
using FinanceManager.Library.Interfaces;
using System.Threading.Tasks;

public class ReportService : IReportService
{
    private ITransactionService _transactionService;

    public ReportService(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    public async Task<DailyReport> GetDailyReportAsync(DateOnly date)
    {
        DailyReport dailyReport = new DailyReport(date, await _transactionService.GetByDateAsync(date));

        return dailyReport;
    }
    public async Task<PeriodReport> GetPeriodReportAsync(DateOnly startDate, DateOnly endDate)
    {
        PeriodReport periodReport = new PeriodReport(startDate, endDate,
                                                     await _transactionService
                                                               .GetByDateAsync(startDate, endDate));

        return periodReport;
    }
}
