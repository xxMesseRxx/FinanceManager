namespace FinanceManager.Model;

public class FinancialOperation
{
    public int Id { get; set; }
    public int Sum { get; set; }
    public string Discription { get; set; }
    public DateTime DateTime { get; set; }
	public int OperationTypeId { get; set; }
    public Operation OperationType { get; set; } = null!;
}
