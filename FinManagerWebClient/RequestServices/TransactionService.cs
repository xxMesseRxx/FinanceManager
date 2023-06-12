namespace FinManagerWebClient.RequestServices;

using FinManagerWebClient.DTO.Transaction;
using FinManagerWebClient.Library.Requests;
using FinManagerWebClient.Model;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class TransactionService : ITransactionService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public TransactionService(IHttpClientFactory httpClientFactory)
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        _baseUrl = config["URLs:FinanceManager:Transactions"];

        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<int> CreateAsync(TransactionCreateDto transactionCreateDto)
    {
        CheckDataForCreate(transactionCreateDto);

        HttpResponseMessage response = await _httpClient.PostAsJsonAsync<TransactionCreateDto>(_baseUrl, transactionCreateDto);

        await CheckSuccessCode(response);

        return await response.Content.ReadFromJsonAsync<int>();
    }

    public async Task<List<TransactionVM>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<TransactionVM>>(_baseUrl);
    }

    public async Task<TransactionVM> GetAsync(int id)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{_baseUrl}/{id}");

        await CheckSuccessCode(response);

        return await response.Content.ReadFromJsonAsync<TransactionVM>();
    }

    public async Task<TransactionVM> RemoveAsync(int id)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");

        await CheckSuccessCode(response);

        return await response.Content.ReadFromJsonAsync<TransactionVM>();
    }

    public async Task<TransactionVM> UpdateAsync(TransactionUpdateDto transactionUpdateDto)
    {
        CheckDataForUpdate(transactionUpdateDto);

        HttpResponseMessage response = await _httpClient.PutAsJsonAsync<TransactionUpdateDto>(_baseUrl, transactionUpdateDto);

        await CheckSuccessCode(response);

        return await response.Content.ReadFromJsonAsync<TransactionVM>();
    }

    private async Task CheckSuccessCode(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            string msg = await response.Content.ReadAsStringAsync();
            throw new InvalidOperationException(msg);
        }
    }

    private void CheckDataForCreate(TransactionCreateDto transactionCreateDto)
    {
        if (transactionCreateDto is null)
        {
            throw new ArgumentNullException(nameof(transactionCreateDto));
        }
        else if (transactionCreateDto.Sum == 0)
        {
            throw new ArgumentNullException(nameof(transactionCreateDto.Sum));
        }
        else if (transactionCreateDto.OperationId == 0)
        {
            throw new ArgumentNullException(nameof(transactionCreateDto.OperationId));
        }
        else if (new DateTime(1900, 01, 01) >= transactionCreateDto.DateTime ||
                 DateTime.Now <= transactionCreateDto.DateTime)
        {
            throw new ArgumentException("Date isn't valid");
        }
    }

    private void CheckDataForUpdate(TransactionUpdateDto transactionUpdateDto)
    {
        if (transactionUpdateDto is null)
        {
            throw new ArgumentNullException(nameof(transactionUpdateDto));
        }
        else if (transactionUpdateDto.Id == 0)
        {
            throw new ArgumentNullException(nameof(transactionUpdateDto.Id));
        }
        else if (transactionUpdateDto.Sum == 0)
        {
            throw new ArgumentNullException(nameof(transactionUpdateDto.Sum));
        }
        else if (transactionUpdateDto.OperationId == 0)
        {
            throw new ArgumentNullException(nameof(transactionUpdateDto.OperationId));
        }
        else if (new DateTime(1900, 01, 01) >= transactionUpdateDto.DateTime ||
                 DateTime.Now <= transactionUpdateDto.DateTime)
        {
            throw new ArgumentException("Date isn't valid");
        }
    }
}
