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

	public async Task<List<OperationViewModel>> GetAllAsync()
	{
		List<Operation> operations = await _db.Operations.ToListAsync();

		List<OperationViewModel> result = operations.Select(o => new OperationViewModel()
		{
			Id = o.Id,
			Name = o.Name,
		}).ToList();

        return result;
	}

	public async Task<OperationViewModel> GetAsync(int id)
	{
		Operation? operation = await _db.Operations.FindAsync(id);

		if (operation is null)
		{
			return null;
		}

		OperationViewModel result = new OperationViewModel()
		{
			Id = operation.Id,
			Name = operation.Name,
		};

        return result;
	}

    public async Task<OperationViewModel> GetAsync(string name)
    {
        Operation? operation = await _db.Operations.FirstOrDefaultAsync(o => o.Name == name);

        if (operation is null)
        {
            return null;
        }

        OperationViewModel result = new OperationViewModel()
        {
            Id = operation.Id,
            Name = operation.Name,
        };

        return result;
    }

    public async Task RemoveAsync(int id)
	{
		Operation? operation = await _db.Operations.FindAsync(id);

		if (operation is null)
		{
            throw new ArgumentException("Operation isn't exist");
        }

		Transaction? transaction = await _db.Transactions.FirstOrDefaultAsync(t => t.OperationId == id);

		if (transaction is not null)
		{
			throw new InvalidOperationException("Operation cannot be deleted when it has got financial operations");
		}

        try
        {
            _db.Operations.Remove(operation);
            _db.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception(ex.Message);
        }
	}
}
