namespace FinanceManager.Services;

using FinanceManager.DAL;
using FinanceManager.Library.Interfaces;
using FinanceManager.Model;
using FinanceManager.DAL.DTO.Transaction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceManager.DAL.DTO.Operation;

public class TransactionService : ITransactionService
{
	private FinanceManagerContext _db;

	public TransactionService(FinanceManagerContext context)
	{
		_db = context;
	}

	public async Task<int> AddAsync(TransactionCreateDto transactionCreateDto)
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
			return newTransaction.Id;
		}
		catch (DbUpdateException)
		{
			throw new ArgumentException("OperationId isn't exist or DateTime isn't valid");
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
        catch (DbUpdateException)
        {
            throw new ArgumentException("OperationId isn't exist or DateTime isn't valid");
        }
    }

	public async Task<List<TransactionViewModel>> GetAllAsync()
	{
		List<Transaction> transactions = await _db.Transactions
                                                    .Include(t => t.Operation)
                                                    .ToListAsync();

        return CreateTransactionsVM(transactions);
	}

	public async Task<TransactionViewModel> GetAsync(int id)
	{
		Transaction? transaction = await _db.Transactions.FindAsync(id);

		if (transaction is null)
		{
			return null;
		}

		TransactionViewModel transactionVM = new TransactionViewModel()
		{
			Id = transaction.Id,
			Sum = transaction.Sum,
			Discription = transaction.Discription,
			DateTime = transaction.DateTime,
			OperationId = transaction.OperationId,
			OperationVM = null,
		};

        return transactionVM;
	}

    public async Task<List<TransactionViewModel>> GetByDateAsync(DateOnly date)
    {
		DateTime dateTime = date.ToDateTime(TimeOnly.MinValue);

        List<Transaction> transactions = await _db.Transactions
												.Where(t => t.DateTime.Date == dateTime.Date)
												.Include(t => t.Operation)
												.ToListAsync();

        return CreateTransactionsVM(transactions);
    }

    public async Task<List<TransactionViewModel>> GetByDateAsync(DateOnly startDate, DateOnly endDate)
    {
        DateTime startDateTime = startDate.ToDateTime(TimeOnly.MinValue);
        DateTime endDateTime = endDate.ToDateTime(TimeOnly.MinValue);

        List<Transaction> transactions = await _db.Transactions
												.Where(t => t.DateTime.Date >= startDateTime.Date &&
															t.DateTime.Date <= endDateTime.Date)
												.Include(t => t.Operation)
												.ToListAsync();

        return CreateTransactionsVM(transactions);
    }

    public async Task<List<TransactionViewModel>> GetWithOperIdAsync(int operationId)
	{
		List<Transaction> transactions = await _db.Transactions
												.Where(t => t.OperationId == operationId)
                                                .Include(t => t.Operation)
                                                .ToListAsync();

		return CreateTransactionsVM(transactions);
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

	private List<TransactionViewModel> CreateTransactionsVM(List<Transaction> transactions)
	{
		List<TransactionViewModel> transactionsVM = transactions.Select(t => new TransactionViewModel()
		{
			Id = t.Id,
			Sum = t.Sum,
			Discription = t.Discription,
			DateTime = t.DateTime,
			OperationId = t.OperationId,
			OperationVM = new OperationViewModel()
			{
				Id = t.Operation.Id,
				Name = t.Operation.Name
			}
		}).ToList();

		return transactionsVM;
	}
}
