namespace FinanceManager.DAL;

using FinanceManager.Library;
using FinanceManager.Library.Interfaces;
using FinanceManager.Model;

public class PeriodReport : Report
{
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }

    public PeriodReport(DateOnly startDate, DateOnly endDate, List<Transaction> transactions)
    {
        StartDate = startDate;
        EndDate = endDate;
        Transactions = transactions;
    }
}
