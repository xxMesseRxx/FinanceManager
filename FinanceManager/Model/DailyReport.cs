namespace FinanceManager.Model;

using FinanceManager.Library;
using FinanceManager.Library.Interfaces;
using FinanceManager.Services;

public class DailyReport : Report
{
    public DateOnly Date { get; private set; }

    public DailyReport(DateOnly date, ITransactionService transactionSerivce) 
    { 
        Date = date;
        Transactions = transactionSerivce.GetTransactionsByDateAsync(date).Result;

        CountTotalExpenses();
        CountTotalIncome();
    }
}
