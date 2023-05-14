namespace FinanceManager.Library;

using FinanceManager.Model;

public abstract class Report
{
    public List<Transaction> Transactions { get; protected set; }
    public int TotalIncome { get; protected set; }
    public int TotalExpenses { get; protected set; }

    protected void CountTotalIncome()
    {
        foreach (var transaction in Transactions)
        {
            if (transaction.Sum > 0)
            {
                TotalIncome += transaction.Sum;
            }
        }
    }
    protected void CountTotalExpenses()
    {
        foreach (var transaction in Transactions)
        {
            if (transaction.Sum < 0)
            {
                TotalExpenses += transaction.Sum;
            }
        }
    }
}
