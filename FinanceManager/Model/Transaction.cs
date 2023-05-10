using System.ComponentModel.DataAnnotations;

namespace FinanceManager.Model;

public class Transaction
{
    public int Id { get; set; }
    public int Sum { get; set; }
    public string? Discription { get; set; }
    [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime DateTime { get; set; }
	public int OperationId { get; set; }
    public Operation Operation { get; set; } = null!;
}
