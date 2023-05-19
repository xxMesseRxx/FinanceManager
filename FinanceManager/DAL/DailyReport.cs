namespace FinanceManager.DAL;

using FinanceManager.Library;
using FinanceManager.Library.Interfaces;
using FinanceManager.Model;
using FinanceManager.Services;

public class DailyReport : Report
{
    public DateOnly Date { get; private set; }

    public DailyReport(DateOnly date, List<Transaction> transactions)
    {
        Date = date;
        Transactions = transactions;
    }
}
