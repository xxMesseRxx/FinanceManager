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
    public static async Task<IResult> AddAsync(IOperationService operationService,
                                               Operation operation)
    {
        try
        {
            await operationService.AddOperationAsync(operation.Name);
            var addedOperation = await operationService.GetOperationAsync(operation.Name);

            return Results.Json(addedOperation);
        }
        catch (ArgumentException)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                {"Validation error", new string[] { "Name is exist" } }
            });
        }
    }
}
