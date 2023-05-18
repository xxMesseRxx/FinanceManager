namespace FinanceManager.Model;

using System.Text.Json.Serialization;

[Serializable]
public class Operation
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    [JsonIgnore]
    public List<Transaction> Transactions { get; set; }
}
