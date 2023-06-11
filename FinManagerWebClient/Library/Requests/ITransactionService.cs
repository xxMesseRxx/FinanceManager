namespace FinManagerWebClient.Library.Requests;

using FinManagerWebClient.DTO.Transaction;
using FinManagerWebClient.Model;

public interface ITransactionService
{
    public Task<List<TransactionVM>> GetAsync();
    public Task<int> CreateAsync(TransactionCreateDto transactionCreateDto);
    public Task<TransactionVM> UpdateAsync(TransactionUpdateDto transactionUpdateDto);
    public Task<TransactionVM> RemoveAsync(int id);
}
