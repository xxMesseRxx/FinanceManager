namespace FinanceManager.DAL.DTO.Reports;

using FinanceManager.DAL.DTO.Transaction;
using FinanceManager.Library;
using FinanceManager.Library.Interfaces;
using FinanceManager.Model;
using FinanceManager.Services;

public class DailyReport : Report
{
    public DateOnly Date { get; private set; }

    public DailyReport(DateOnly date, List<TransactionViewModel> transactionsVM)
    {
        Date = date;
        TransactionsVM = transactionsVM;
    }
}
