namespace FinanceManager.Model;

public class Operation
{
    public int Id { get; set; }
    public string Type { get; set; } = null!;
    public List<FinancialOperation> FinancialOperations { get; set; }
}
