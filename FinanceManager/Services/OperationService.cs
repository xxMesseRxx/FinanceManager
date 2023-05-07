namespace FinanceManager.Services;

using FinanceManager.Library.Interfaces;
using FinanceManager.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class OperationService : IOperationService
{
	private FinanceManagerContext _db;

	public OperationService(FinanceManagerContext context)
	{
		_db = context;
	}

	public async Task AddOperationAsync(string name)
	{
		Operation newOperation = new Operation() { Name = name };

		try
		{
			_db.Operations.Add(newOperation);
			_db.SaveChanges();
		}
		catch (DbUpdateException)
		{
			throw new ArgumentException("Name isn't unique");
		}
	}
	public async Task EditOperationAsync(int id, string name)
	{
		Operation? operation = await _db.Operations.FindAsync(id);

		if (operation is not null)
		{
			operation.Name = name;

			try
			{
				_db.Operations.Update(operation);
				_db.SaveChanges();
			}
			catch (DbUpdateException)
			{
				throw new ArgumentException("Name isn't unique");
			}
		}
		else
		{
			throw new ArgumentException("Operation isn't exist");
		}
	}
	public async Task<List<Operation>> GetAllAsync()
	{
		return _db.Operations.ToList();
	}
	public async Task<Operation> GetOperationAsync(int id)
	{
		return await _db.Operations.FindAsync(id);
	}
	public async Task RemoveOperationAsync(int id)
	{
		Operation? operation = await _db.Operations.FindAsync(id);

		if (operation is not null)
		{
			try
			{
				_db.Operations.Remove(operation);
				_db.SaveChanges();
			}
			catch (DbUpdateException)
			{
				throw new ArgumentException("Operation cannot be deleted when it has got financial operations");
			}
		}
		else
		{
			throw new ArgumentException("Operation isn't exist");
		}
	}
}
