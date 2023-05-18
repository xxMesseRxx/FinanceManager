namespace FinanceManager.Library.Interfaces;

using FinanceManager.DAL.DTO.Transaction;
using FinanceManager.Model;

public interface ITransactionService
{
	public Task<List<Transaction>> GetAllAsync();
	public Task<Transaction> GetTransactionAsync(int id);
	public Task<List<Transaction>> GetTransactionsByDateAsync(DateOnly date);
    public Task<List<Transaction>> GetTransactionsByDateAsync(DateOnly startDate, DateOnly endDate);
    public Task<List<Transaction>> GetTransactionWithOperIdAsync(int operationId);
	public Task RemoveTransactionAsync(int id);
	public Task EditTransactionAsync(TransactionUpdateDto transactionUpdateDto);
	public Task AddTransactionAsync(TransactionCreateDto transactionCreateDto);
}
