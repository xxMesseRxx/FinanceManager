namespace FinanceManager.Library.Interfaces;

using FinanceManager.DAL.DTO.Operation;
using FinanceManager.Model;

public interface IOperationService
{
	public Task<List<Operation>> GetAllAsync();
	public Task<Operation> GetOperationAsync(int id);
    public Task<Operation> GetOperationAsync(string name);
    public Task RemoveOperationAsync(int id);
	public Task EditOperationAsync(OperationUpdateDto operationUpdateDto);
	public Task AddOperationAsync(OperationCreateDto operationCreateDto);
}
