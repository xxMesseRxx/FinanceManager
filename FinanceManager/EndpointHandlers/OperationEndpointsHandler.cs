﻿namespace FinanceManager.EndpointHandlers;

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
        if (operation.Id != 0)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                {"Validation error", new string[] { $"Id must be empty" } }
            });
        }
        else if (operation.Transactions is not null)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                {"Validation error", new string[] { $"Transactions must be empty" } }
            });
        }

        try
        {
            await operationService.AddOperationAsync(operation.Name);
            var addedOperation = await operationService.GetOperationAsync(operation.Name);

            return Results.Json(addedOperation);
        }
        catch (ArgumentException ex)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                {"Validation error", new string[] { ex.Message } }
            });
        }
    }
    public static async Task<IResult> EditAsync(IOperationService operationService,
                                                Operation operation)
    {
        try
        {
            await operationService.EditOperationAsync(operation.Id, operation.Name);
            var editedOperation = await operationService.GetOperationAsync(operation.Id);

            return Results.Json(editedOperation);
        }
        catch (ArgumentException ex)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                {"Validation error", new string[] { ex.Message } }
            });
        }
    }
    public static async Task<IResult> RemoveAsync(IOperationService operationService,
                                                  int id)
    {
        try
        {
            var removedOperation = await operationService.GetOperationAsync(id);
            await operationService.RemoveOperationAsync(id);

            return Results.Json(removedOperation);
        }
        catch (ArgumentException ex)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                {"Validation error", new string[] { ex.Message } }
            });
        }
    }
}
