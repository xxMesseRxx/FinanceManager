namespace FinanceManager.Controllers;

using FinanceManager.Library.Interfaces;
using FinanceManager.Model;
using FinanceManager.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class OperationsController : ControllerBase
{
    private readonly IOperationService _operationService;

    public OperationsController (IOperationService operationService)
    {
        _operationService = operationService;
    }

    [HttpGet]
    public async Task<List<Operation>> GetAsync()
    {
        return await _operationService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        Operation operation = await _operationService.GetOperationAsync(id);

        if (operation is null)
        {
            return NotFound(new { message = "Operation not found" });
        }

        return new ObjectResult(operation);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] Operation operation)
    {
        if (operation.Id != 0)
        {
            return BadRequest(new { message = "Id must be empty" });
        }

        try
        {
            await _operationService.AddOperationAsync(operation.Name);

            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut]
    public async Task<IActionResult> PutAsync([FromBody] Operation operation)
    {
        try
        {
            await _operationService.EditOperationAsync(operation.Id, operation.Name);
            var editedOperation = await _operationService.GetOperationAsync(operation.Id);

            return new ObjectResult(editedOperation);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            var removedOperation = await _operationService.GetOperationAsync(id);
            await _operationService.RemoveOperationAsync(id);

            return new ObjectResult(removedOperation);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
