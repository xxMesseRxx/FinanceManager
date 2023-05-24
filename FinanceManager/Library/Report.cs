namespace FinanceManager.Library;

using FinanceManager.DAL.DTO.Transaction;
using FinanceManager.Model;

public abstract class Report
{
    public List<TransactionViewModel> TransactionsVM 
    {
        get
        {
            return _transactionsVM;
        }
        protected set
        {
            _transactionsVM = value;
            CountTotalExpenses();
            CountTotalIncome();
        }
    }
    public int TotalIncome { get; protected set; }
    public int TotalExpenses { get; protected set; }

    private List<TransactionViewModel> _transactionsVM;

    protected void CountTotalIncome()
    {
        TotalIncome = TransactionsVM.Where(t => t.Sum > 0)
                                    .Sum(t => t.Sum);   
    }
    protected void CountTotalExpenses()
    {
        TotalExpenses = TransactionsVM.Where(t => t.Sum < 0)
                                      .Sum(t => t.Sum);
    }
}
