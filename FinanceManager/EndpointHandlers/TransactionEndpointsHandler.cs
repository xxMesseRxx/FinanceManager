namespace FinanceManager.EndpointHandlers;

using FinanceManager.Library.Interfaces;
using FinanceManager.Model;
using FinanceManager.Services;

public static class TransactionEndpointsHandler
{
	public static async Task<List<Transaction>> GetAllAsync(ITransactionService transactionService)
	{
		return await transactionService.GetAllAsync();
	}
}
