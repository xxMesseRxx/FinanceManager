namespace FinanceManager.Services;

using FinanceManager.DAL;
using FinanceManager.DAL.DTO.Operation;
using FinanceManager.Library.Interfaces;
using FinanceManager.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Threading.Tasks;

public class OperationService : IOperationService
{
	private FinanceManagerContext _db;

	public OperationService(FinanceManagerContext context)
	{
		_db = context;
	}

	public async Task AddOperationAsync(OperationCreateDto operationCreateDto)
	{
		if (string.IsNullOrEmpty(operationCreateDto.Name))
		{
			throw new ArgumentNullException(nameof(operationCreateDto.Name));
		}

		Operation newOperation = new Operation() { Name = operationCreateDto.Name };
		 
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
	public async Task EditOperationAsync(OperationUpdateDto operationUpdateDto)
	{
        if (string.IsNullOrEmpty(operationUpdateDto.Name))
        {
            throw new ArgumentNullException(nameof(operationUpdateDto.Name));
        }

        Operation? operation = await _db.Operations.FindAsync(operationUpdateDto.Id);

		if (operation is not null)
		{
			operation.Name = operationUpdateDto.Name;

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
    public async Task<Operation> GetOperationAsync(string name)
    {
        return await _db.Operations.FirstOrDefaultAsync(o => o.Name == name);
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
