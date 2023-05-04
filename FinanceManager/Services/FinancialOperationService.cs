namespace FinanceManager.Services;

using FinanceManager.Library.Interfaces;
using FinanceManager.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class FinancialOperationService : IFinancialOperationService
{
	private FinanceManagerContext _db;

	public FinancialOperationService(FinanceManagerContext context)
	{
		_db = context;
	}

	public async Task AddFinOperAsync(int sum, string discription, DateTime dateTime, int operationTypeId)
	{
		FinancialOperation newFinOperation = new FinancialOperation() { Sum = sum, Discription = discription,
																		DateTime = dateTime,
																		OperationTypeId = operationTypeId};
		try
		{
			_db.FinancialOperations.Add(newFinOperation);
			_db.SaveChanges();
		}
		catch (DbUpdateException)
		{
			throw new ArgumentException();
		}
	}
	public async Task EditFinOperAsync(int id, int sum, string discription, DateTime dateTime, int operationTypeId)
	{
		FinancialOperation? finOperation = await _db.FinancialOperations.FindAsync(id);

		if (finOperation is not null)
		{
			finOperation.Sum = sum;
			finOperation.Discription = discription;
			finOperation.DateTime = dateTime;
			finOperation.OperationTypeId = operationTypeId;

			try
			{
				_db.FinancialOperations.Update(finOperation);
				_db.SaveChanges();
			}
			catch (DbUpdateException)
			{
				throw new ArgumentException();
			}
		}
		else
		{
			throw new ArgumentException("Financial operation isn't exist");
		}
	}
	public async Task<List<FinancialOperation>> GetAllAsync()
	{
		return _db.FinancialOperations.ToList();
	}
	public async Task<FinancialOperation> GetFinOperationAsync(int id)
	{
		return await _db.FinancialOperations.FindAsync(id);
	}
	public async Task<List<FinancialOperation>> GetFinOperWithOperTypeAsync(int operationTypeId)
	{
		List<FinancialOperation> finOperations = await _db.FinancialOperations
															.Where(f => f.OperationTypeId == operationTypeId)
															.ToListAsync();

		return finOperations;
	}
	public async Task RemoveFinOperAsync(int id)
	{
		FinancialOperation? finOperation = await _db.FinancialOperations.FindAsync(id);

		if (finOperation is null)
		{
			throw new ArgumentException("Financial operation isn't exist");
		}

		_db.FinancialOperations.Remove(finOperation);
	}
}
