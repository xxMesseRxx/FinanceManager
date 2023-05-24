namespace FinanceManager.Library.Interfaces;

using FinanceManager.DAL.DTO.Operation;
using FinanceManager.Model;

public interface IOperationService
{
	public Task<List<OperationViewModel>> GetAllAsync();
	public Task<OperationViewModel> GetAsync(int id);
    public Task<OperationViewModel> GetAsync(string name);
    public Task RemoveAsync(int id);
	public Task EditAsync(OperationUpdateDto operationUpdateDto);
	public Task AddAsync(OperationCreateDto operationCreateDto);
}
