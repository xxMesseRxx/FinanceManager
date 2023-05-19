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

	public async Task AddAsync(OperationCreateDto operationCreateDto)
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

	public async Task EditAsync(OperationUpdateDto operationUpdateDto)
	{
        if (string.IsNullOrEmpty(operationUpdateDto.Name))
        {
            throw new ArgumentNullException(nameof(operationUpdateDto.Name));
        }

        Operation? operation = await _db.Operations.FindAsync(operationUpdateDto.Id);

		if (operation is null)
		{
            throw new ArgumentException("Operation isn't exist");
        }

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

	public async Task<List<Operation>> GetAllAsync()
	{
		return _db.Operations.ToList();
	}

	public async Task<Operation> GetAsync(int id)
	{
		return await _db.Operations.FindAsync(id);
	}

    public async Task<Operation> GetAsync(string name)
    {
        return await _db.Operations.FirstOrDefaultAsync(o => o.Name == name);
    }

    public async Task RemoveAsync(int id)
	{
		Operation? operation = await _db.Operations.FindAsync(id);

		if (operation is null)
		{
            throw new ArgumentException("Operation isn't exist");
        }

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
}
