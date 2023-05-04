namespace FinanceManager.Library.Interfaces;

using FinanceManager.Model;

public interface IFinancialOperationService
{
	public Task<List<FinancialOperation>> GetAllAsync();
	public Task<List<FinancialOperation>> GetFinOperWithOperTypeAsync(int operationTypeId);
	public Task RemoveFinOperAsync(int id);
	public Task EditFinOperAsync(int id, int sum, string discription, 
								 DateTime dateTime, int operationTypeId);
	public Task AddFinOperAsync(int sum, string discription,
								 DateTime dateTime, int operationTypeId);

}
