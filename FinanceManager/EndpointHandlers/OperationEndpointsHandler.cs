namespace FinanceManager.EndpointHandlers;

using FinanceManager.Library.Interfaces;
using FinanceManager.Model;

public static class OperationEndpointsHandler
{
    public static async Task<IResult> GetAllAsync(IOperationService operationService)
    {
        List<Operation> operations = await operationService.GetAllAsync();

        return Results.Json(operations);
    }
    public static async Task<IResult> GetOperationAsync(IOperationService operationService,
                                                        int id)
    {
        Operation operation = await operationService.GetOperationAsync(id);

        if (operation is null)
        {
            return Results.NotFound(new { message = "Операция не найдена" });
        }

        return Results.Json(operation);
    }
}
