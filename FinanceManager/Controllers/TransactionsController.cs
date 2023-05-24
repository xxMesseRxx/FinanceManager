namespace FinanceManager.Controllers;

using FinanceManager.Library.Interfaces;
using FinanceManager.DAL.DTO.Transaction;
using FinanceManager.Model;
using FinanceManager.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionsController (ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet]
    public async Task<List<TransactionViewModel>> GetAsync()
    {
        return await _transactionService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute] int id)
    {
        TransactionViewModel transactionVM = await _transactionService.GetAsync(id);

        if (transactionVM is null)
        {
            return NotFound(new { message = "Transaction not found" });
        }

        return Ok(transactionVM);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] TransactionCreateDto transactionCreateDto)
    {
        try
        {
            await _transactionService.AddAsync(transactionCreateDto);

            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut]
    public async Task<IActionResult> PutAsync([FromBody] TransactionUpdateDto transactionUpdateDto)
    {
        try
        {
            await _transactionService.EditAsync(transactionUpdateDto);
            var editedTransactionVM = await _transactionService.GetAsync(transactionUpdateDto.Id);

            return Ok(editedTransactionVM);
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
            var removedTransactionVM = await _transactionService.GetAsync(id);
            await _transactionService.RemoveAsync(id);

            return Ok(removedTransactionVM);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
