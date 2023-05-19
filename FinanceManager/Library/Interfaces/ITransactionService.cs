namespace FinanceManager.Library.Interfaces;

using FinanceManager.DAL.DTO.Transaction;
using FinanceManager.Model;

public interface ITransactionService
{
	public Task<List<Transaction>> GetAllAsync();
	public Task<Transaction> GetAsync(int id);
	public Task<List<Transaction>> GetByDateAsync(DateOnly date);
    public Task<List<Transaction>> GetByDateAsync(DateOnly startDate, DateOnly endDate);
    public Task<List<Transaction>> GetWithOperIdAsync(int operationId);
	public Task RemoveAsync(int id);
	public Task EditAsync(TransactionUpdateDto transactionUpdateDto);
	public Task AddAsync(TransactionCreateDto transactionCreateDto);
}
