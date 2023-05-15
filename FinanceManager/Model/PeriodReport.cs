namespace FinanceManager.Model;

using FinanceManager.Library;
using FinanceManager.Library.Interfaces;

public class PeriodReport : Report
{
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }

    public PeriodReport(DateOnly startDate, DateOnly endDate, ITransactionService transactionService)
    {
        StartDate = startDate;
        EndDate = endDate;
        Transactions = transactionService.GetTransactionsByDateAsync(startDate, endDate).Result;

        CountTotalExpenses();
        CountTotalIncome();
    }
}
