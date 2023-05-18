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
    public async Task<List<Transaction>> GetAsync()
    {
        return await _transactionService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        Transaction transaction = await _transactionService.GetTransactionAsync(id);

        if (transaction is null)
        {
            return NotFound(new { message = "Transaction not found" });
        }

        return new ObjectResult(transaction);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] TransactionCreateDto transactionCreateDto)
    {
        try
        {
            await _transactionService.AddTransactionAsync(transactionCreateDto);

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
            await _transactionService.EditTransactionAsync(transactionUpdateDto);
            var editedTransaction = await _transactionService.GetTransactionAsync(transactionUpdateDto.Id);

            return new ObjectResult(editedTransaction);
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
            var removedTransaction = await _transactionService.GetTransactionAsync(id);
            await _transactionService.RemoveTransactionAsync(id);

            return new ObjectResult(removedTransaction);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
