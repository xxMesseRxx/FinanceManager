namespace FinManagerWebClient.Library.Requests;

using FinManagerWebClient.DTO;
using FinManagerWebClient.Model;

public interface IOperationRequests
{
    public Task<List<OperationVM>> GetAsync();
    public Task CreateAsync(OperationVM operation);
    public Task<OperationVM> UpdateAsync(OperationVM operation);
    public Task<OperationVM> RemoveAsync(int id);
}
