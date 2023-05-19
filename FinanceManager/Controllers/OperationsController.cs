﻿namespace FinanceManager.Controllers;

using FinanceManager.Library.Interfaces;
using FinanceManager.Model;
using FinanceManager.Services;
using Microsoft.AspNetCore.Mvc;
using FinanceManager.DAL.DTO.Operation;

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
        Operation operation = await _operationService.GetAsync(id);

        if (operation is null)
        {
            return NotFound(new { message = "Operation not found" });
        }

        return new ObjectResult(operation);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] OperationCreateDto operationCreateDto)
    {
        try
        {
            await _operationService.AddAsync(operationCreateDto);

            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut]
    public async Task<IActionResult> PutAsync([FromBody] OperationUpdateDto operationUpdateDto)
    {
        try
        {
            await _operationService.EditAsync(operationUpdateDto);
            var editedOperation = await _operationService.GetAsync(operationUpdateDto.Id);

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
            var removedOperation = await _operationService.GetAsync(id);
            await _operationService.RemoveAsync(id);

            return new ObjectResult(removedOperation);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
