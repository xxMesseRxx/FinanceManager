namespace FinanceManager.Library;

using FinanceManager.Model;

public abstract class Report
{
    public List<Transaction> Transactions 
    {
        get
        {
            return _transactions;
        }
        protected set
        {
            _transactions = value;
            CountTotalExpenses();
            CountTotalIncome();
        }
    }
    public int TotalIncome { get; protected set; }
    public int TotalExpenses { get; protected set; }

    private List<Transaction> _transactions;

    protected void CountTotalIncome()
    {
        TotalIncome = Transactions.Where(t => t.Sum > 0)
                                  .Sum(t => t.Sum);   
    }
    protected void CountTotalExpenses()
    {
        TotalExpenses = Transactions.Where(t => t.Sum < 0)
                                    .Sum(t => t.Sum);
    }
}
