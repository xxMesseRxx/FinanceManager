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
    public async Task<List<OperationViewModel>> GetAsync()
    {
        return await _operationService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute] int id)
    {
        OperationViewModel operationVM = await _operationService.GetAsync(id);

        if (operationVM is null)
        {
            return NotFound(new { message = "Operation not found" });
        }

        return Ok(operationVM);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] OperationCreateDto operationCreateDto)
    {
        try
        {
            int id = await _operationService.AddAsync(operationCreateDto);

            return Ok(id);
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
            var editedOperationVM = await _operationService.GetAsync(operationUpdateDto.Id);

            return Ok(editedOperationVM);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            var removedOperationVM = await _operationService.GetAsync(id);
            await _operationService.RemoveAsync(id);

            return Ok(removedOperationVM);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
