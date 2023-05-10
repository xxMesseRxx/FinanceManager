namespace FinanceManager.EndpointHandlers;

using FinanceManager.Library.Interfaces;
using FinanceManager.Model;
using FinanceManager.Services;

public static class TransactionEndpointsHandler
{
	public static async Task<IResult> GetAllAsync(ITransactionService transactionService)
	{
		List<Transaction> transactions = await transactionService.GetAllAsync();

		return Results.Json(transactions);
	}
    public static async Task<IResult> GetTransactionAsync(ITransactionService transactionService,
                                                          int id)
    {
        Transaction transaction = await transactionService.GetTransactionAsync(id);

		if (transaction is null)
		{
			return Results.NotFound(new { message = "Transaction not found" });
		}

        return Results.Json(transaction);
    }
    public static async Task<IResult> AddAsync(ITransactionService transactionService,
                                               Transaction transaction)
    {
        if (transaction.Id != 0)
        {
            return Results.BadRequest(new { message = "Id must be empty" });
        }

        try
        {
            await transactionService.AddTransactionAsync(transaction.Sum, transaction.Discription,
                                                         transaction.DateTime, transaction.OperationId);

            return Results.Ok();
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(new { message = ex.Message });
        }
    }
}
