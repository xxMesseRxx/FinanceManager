namespace FinManagerWebClient.Library;

using FinManagerWebClient.Model;

public abstract class Report
{
    public List<TransactionVM> TransactionsVM { get; set; }
    public int TotalIncome { get; set; }
    public int TotalExpenses { get; set; }
}
