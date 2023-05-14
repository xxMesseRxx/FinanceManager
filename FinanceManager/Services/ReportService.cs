namespace FinanceManager.Services;

using FinanceManager.Library.Interfaces;
using FinanceManager.Model;
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
        DailyReport dailyReport = new DailyReport(date, _transactionService);

        return dailyReport;
    }
}
