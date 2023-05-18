namespace FinanceManager.Services;

using FinanceManager.DAL;
using FinanceManager.Library.Interfaces;
using FinanceManager.Model;
using FinanceManager.DAL.DTO.Transaction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TransactionService : ITransactionService
{
	private FinanceManagerContext _db;

	public TransactionService(FinanceManagerContext context)
	{
		_db = context;
	}

	public async Task AddTransactionAsync(TransactionCreateDto transactionCreateDto)
	{
		Transaction newTransaction = new Transaction()
		{ 
			Sum = transactionCreateDto.Sum,
			Discription = transactionCreateDto.Discription,
			DateTime = transactionCreateDto.DateTime,
			OperationId = transactionCreateDto.OperationId
        };

		try
		{
			_db.Transactions.Add(newTransaction);
			_db.SaveChanges();
		}
		catch (DbUpdateException ex)
		{
			throw new ArgumentException(ex.Message);
		}
	}
	public async Task EditTransactionAsync(TransactionUpdateDto transactionUpdateDto)
	{
		Transaction? transaction = await _db.Transactions.FindAsync(transactionUpdateDto.Id);

		if (transaction is not null)
		{
			transaction.Sum = transactionUpdateDto.Sum;
			transaction.Discription = transactionUpdateDto.Discription;
			transaction.DateTime = transactionUpdateDto.DateTime;
			transaction.OperationId = transactionUpdateDto.OperationId;

			try
			{
				_db.Transactions.Update(transaction);
				_db.SaveChanges();
			}
			catch (DbUpdateException ex)
			{
				throw new ArgumentException(ex.Message);
			}
		}
		else
		{
			throw new ArgumentException("Transaction isn't exist");
		}
	}
	public async Task<List<Transaction>> GetAllAsync()
	{
		return _db.Transactions.ToList();
	}
	public async Task<Transaction> GetTransactionAsync(int id)
	{
		return await _db.Transactions.FindAsync(id);
	}
    public async Task<List<Transaction>> GetTransactionsByDateAsync(DateOnly date)
    {
		DateTime dateTime = date.ToDateTime(TimeOnly.MinValue);

		return await _db.Transactions
						.Where(t => t.DateTime.Date == dateTime.Date)
						.Include(t => t.Operation)
						.ToListAsync();
    }
    public async Task<List<Transaction>> GetTransactionsByDateAsync(DateOnly startDate, DateOnly endDate)
    {
        DateTime startDateTime = startDate.ToDateTime(TimeOnly.MinValue);
        DateTime endDateTime = endDate.ToDateTime(TimeOnly.MinValue);

        return await _db.Transactions
                        .Where(t => t.DateTime.Date >= startDateTime.Date &&
									t.DateTime.Date <= endDateTime.Date)
                        .Include(t => t.Operation)
                        .ToListAsync();
    }
    public async Task<List<Transaction>> GetTransactionWithOperIdAsync(int operationId)
	{
		List<Transaction> transaction = await _db.Transactions
												.Where(t => t.OperationId == operationId)
												.ToListAsync();

		return transaction;
	}
	public async Task RemoveTransactionAsync(int id)
	{
		Transaction? transaction = await _db.Transactions.FindAsync(id);

		if (transaction is null)
		{
			throw new ArgumentException("Transaction isn't exist");
		}

		_db.Transactions.Remove(transaction);
		_db.SaveChanges();
	}
}
