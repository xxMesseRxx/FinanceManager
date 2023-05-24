namespace FinanceManager.DAL.DTO.Reports;

using FinanceManager.DAL.DTO.Transaction;
using FinanceManager.Library;
using FinanceManager.Library.Interfaces;
using FinanceManager.Model;

public class PeriodReport : Report
{
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }

    public PeriodReport(DateOnly startDate, DateOnly endDate, List<TransactionViewModel> transactionsVM)
    {
        StartDate = startDate;
        EndDate = endDate;
        TransactionsVM = transactionsVM;
    }
}
