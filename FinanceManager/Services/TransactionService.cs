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

	public async Task AddAsync(TransactionCreateDto transactionCreateDto)
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

	public async Task EditAsync(TransactionUpdateDto transactionUpdateDto)
	{
		Transaction? transaction = await _db.Transactions.FindAsync(transactionUpdateDto.Id);

		if (transaction is null)
		{
            throw new ArgumentException("Transaction isn't exist");
        }

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

	public async Task<List<Transaction>> GetAllAsync()
	{
		return _db.Transactions.ToList();
	}

	public async Task<Transaction> GetAsync(int id)
	{
		return await _db.Transactions.FindAsync(id);
	}

    public async Task<List<Transaction>> GetByDateAsync(DateOnly date)
    {
		DateTime dateTime = date.ToDateTime(TimeOnly.MinValue);

		return await _db.Transactions
						.Where(t => t.DateTime.Date == dateTime.Date)
						.Include(t => t.Operation)
						.ToListAsync();
    }

    public async Task<List<Transaction>> GetByDateAsync(DateOnly startDate, DateOnly endDate)
    {
        DateTime startDateTime = startDate.ToDateTime(TimeOnly.MinValue);
        DateTime endDateTime = endDate.ToDateTime(TimeOnly.MinValue);

        return await _db.Transactions
                        .Where(t => t.DateTime.Date >= startDateTime.Date &&
									t.DateTime.Date <= endDateTime.Date)
                        .Include(t => t.Operation)
                        .ToListAsync();
    }

    public async Task<List<Transaction>> GetWithOperIdAsync(int operationId)
	{
		List<Transaction> transaction = await _db.Transactions
												.Where(t => t.OperationId == operationId)
												.ToListAsync();

		return transaction;
	}

	public async Task RemoveAsync(int id)
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
