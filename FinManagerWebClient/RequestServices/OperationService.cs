namespace FinManagerWebClient.RequestServices;

using FinManagerWebClient.DTO.Operation;
using FinManagerWebClient.Library.Requests;
using FinManagerWebClient.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

public class OperationService : IOperationService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public OperationService(IHttpClientFactory httpClientFactory)
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        _baseUrl = config["URLs:FinanceManager:Operations"];

        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<int> CreateAsync(OperationCreateDto operationCreateDto)
    {
        CheckDataForCreate(operationCreateDto);

        HttpResponseMessage response = await _httpClient.PostAsJsonAsync<OperationCreateDto>(_baseUrl, operationCreateDto);

        await CheckSuccessCode(response);

        return await response.Content.ReadFromJsonAsync<int>();
    }

    public async Task<List<OperationVM>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<OperationVM>>(_baseUrl);
    }

    public async Task<OperationVM> GetAsync(int id)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{_baseUrl}/{id}");

        await CheckSuccessCode(response);

        return await response.Content.ReadFromJsonAsync<OperationVM>();
    }

    public async Task<OperationVM> RemoveAsync(int id)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");

        await CheckSuccessCode(response);

        return await response.Content.ReadFromJsonAsync<OperationVM>();
    }

    public async Task<OperationVM> UpdateAsync(OperationUpdateDto operationUpdateDto)
    {
        CheckDataForUpdate(operationUpdateDto);

        HttpResponseMessage response = await _httpClient.PutAsJsonAsync<OperationUpdateDto>(_baseUrl, operationUpdateDto);

        await CheckSuccessCode(response);

        return await response.Content.ReadFromJsonAsync<OperationVM>();
    }

    private async Task CheckSuccessCode(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            string msg = await response.Content.ReadAsStringAsync();
            throw new InvalidOperationException(msg);
        }
    }

    private void CheckDataForUpdate(OperationUpdateDto operationUpdateDto)
    {
        if (operationUpdateDto is null)
        {
            throw new ArgumentNullException(nameof(operationUpdateDto));
        }
        else if (string.IsNullOrEmpty(operationUpdateDto.Name))
        {
            throw new ArgumentNullException(nameof(operationUpdateDto.Name));
        }
        else if (operationUpdateDto.Id == 0)
        {
            throw new ArgumentException("Id should not be null");
        }
    }

    private void CheckDataForCreate(OperationCreateDto operationCreateDto)
    {
        if (operationCreateDto is null)
        {
            throw new ArgumentNullException(nameof(operationCreateDto));
        }
        else if (string.IsNullOrEmpty(operationCreateDto.Name))
        {
            throw new ArgumentNullException(nameof(operationCreateDto.Name));
        }
    }
}
