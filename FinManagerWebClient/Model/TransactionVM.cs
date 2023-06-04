namespace FinManagerWebClient.Model;

public class TransactionVM
{
    public int Id { get; set; }
    public double Sum { get; set; }
    public string? Discription { get; set; }
    public DateTime DateTime { get; set; }
    public int OperationId { get; set; }
    public OperationVM OperationVM { get; set; }
}
