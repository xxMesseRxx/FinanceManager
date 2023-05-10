namespace FinanceManager.Services;

using FinanceManager.Library.Interfaces;
using FinanceManager.Model;
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

	public async Task AddTransactionAsync(int sum, string discription, DateTime dateTime, int operationId)
	{
		Transaction newTransaction = new Transaction() { Sum = sum, Discription = discription,
																		DateTime = dateTime,
																		OperationId = operationId};
		try
		{
			_db.Transactions.Add(newTransaction);
			_db.SaveChanges();
		}
		catch (DbUpdateException)
		{
			throw new ArgumentException("OperationId isn't exist");
		}
	}
	public async Task EditTransactionAsync(int id, int sum, string discription, DateTime dateTime, int operationId)
	{
		Transaction? transaction = await _db.Transactions.FindAsync(id);

		if (transaction is not null)
		{
			transaction.Sum = sum;
			transaction.Discription = discription;
			transaction.DateTime = dateTime;
			transaction.OperationId = operationId;

			try
			{
				_db.Transactions.Update(transaction);
				_db.SaveChanges();
			}
			catch (DbUpdateException)
			{
				throw new ArgumentException("Operation Id isn't exist");
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
