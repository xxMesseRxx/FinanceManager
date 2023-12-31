﻿namespace FinManagerWebClient.Library.Requests;

using FinManagerWebClient.DTO.Operation;
using FinManagerWebClient.Model;

public interface IOperationService
{
    public Task<List<OperationVM>> GetAllAsync();
    public Task<int> CreateAsync(OperationCreateDto operationCreateDto);
    public Task<OperationVM> UpdateAsync(OperationUpdateDto operationUpdateDto);
    public Task<OperationVM> RemoveAsync(int id);
    public Task<OperationVM> GetAsync(int id);
}
