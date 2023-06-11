namespace FinManagerWebClient.Library.Requests;

using FinManagerWebClient.DTO.Operation;
using FinManagerWebClient.Model;

public interface IOperationRequests
{
    public Task<List<OperationVM>> GetAsync();
    public Task<int> CreateAsync(OperationCreateDto operationCreateDto);
    public Task<OperationVM> UpdateAsync(OperationUpdateDto operationUpdateDto);
    public Task<OperationVM> RemoveAsync(int id);
}
