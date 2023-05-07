namespace FinanceManager.Library.Interfaces;

using FinanceManager.Model;

public interface IOperationService
{
	public Task<List<Operation>> GetAllAsync();
	public Task<Operation> GetOperationAsync(int id);
	public Task RemoveOperationAsync(int id);
	public Task EditOperationAsync(int id, string name);
	public Task AddOperationAsync(string name);
}
