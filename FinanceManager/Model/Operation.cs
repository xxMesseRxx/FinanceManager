namespace FinanceManager.Model;

public class Operation
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Transaction> Transactions { get; set; }
}
