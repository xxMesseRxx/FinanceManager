namespace FinManagerWebClient.DTO.Transaction;

public class TransactionCreateDto
{
    public int Sum { get; set; }
    public string? Discription { get; set; }
    public DateTime DateTime { get; set; }
    public int OperationId { get; set; }
}
