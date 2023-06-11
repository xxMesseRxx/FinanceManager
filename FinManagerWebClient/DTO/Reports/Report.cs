namespace FinManagerWebClient.DTO.Reports;

using FinManagerWebClient.Model;

public abstract class Report
{
    public List<TransactionVM> TransactionsVM { get; set; }
    public double TotalIncome { get; set; }
    public double TotalExpenses { get; set; }
}
