namespace FinanceManager.Library.Interfaces;

using FinanceManager.Model;

public interface ITransactionService
{
	public Task<List<Transaction>> GetAllAsync();
	public Task<Transaction> GetTransactionAsync(int id);
	public Task<List<Transaction>> GetTransactionsByDateAsync(DateOnly date);
    public Task<List<Transaction>> GetTransactionsByDateAsync(DateOnly startDate, DateOnly endDate);
    public Task<List<Transaction>> GetTransactionWithOperIdAsync(int operationId);
	public Task RemoveTransactionAsync(int id);
	public Task EditTransactionAsync(int id, int sum, string discription, 
									 DateTime dateTime, int operationId);
	public Task AddTransactionAsync(int sum, string discription,
									DateTime dateTime, int operationId);
}
