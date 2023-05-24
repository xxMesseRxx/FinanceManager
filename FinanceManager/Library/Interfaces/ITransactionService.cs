namespace FinanceManager.Library.Interfaces;

using FinanceManager.DAL.DTO.Transaction;
using FinanceManager.Model;

public interface ITransactionService
{
	public Task<List<TransactionViewModel>> GetAllAsync();
	public Task<TransactionViewModel> GetAsync(int id);
	public Task<List<TransactionViewModel>> GetByDateAsync(DateOnly date);
    public Task<List<TransactionViewModel>> GetByDateAsync(DateOnly startDate, DateOnly endDate);
    public Task<List<TransactionViewModel>> GetWithOperIdAsync(int operationId);
	public Task RemoveAsync(int id);
	public Task EditAsync(TransactionUpdateDto transactionUpdateDto);
	public Task AddAsync(TransactionCreateDto transactionCreateDto);
}
