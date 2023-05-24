namespace FinanceManager.DAL.DTO.Transaction;

using FinanceManager.DAL.DTO.Operation;
using FinanceManager.Model;

public class TransactionViewModel
{
    public int Id { get; set; }
    public int Sum { get; set; }
    public string? Discription { get; set; }
    public DateTime DateTime { get; set; }
    public int OperationId { get; set; }
    public OperationViewModel OperationVM { get; set; }
}
