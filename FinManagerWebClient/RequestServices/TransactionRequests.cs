namespace FinManagerWebClient.RequestServices;

using FinManagerWebClient.DTO.Transaction;
using FinManagerWebClient.Library.Requests;
using FinManagerWebClient.Model;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class TransactionRequests : ITransactionRequests
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public TransactionRequests(IHttpClientFactory httpClientFactory)
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        _baseUrl = config["URLs:FinanceManager:Transactions"];

        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task CreateAsync(TransactionCreateDto transactionCreateDto)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync<TransactionCreateDto>(_baseUrl, transactionCreateDto);

        await CheckSuccessCode(response);
    }

    public async Task<List<TransactionVM>> GetAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<TransactionVM>>(_baseUrl);
    }

    public async Task<TransactionVM> RemoveAsync(int id)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");

        await CheckSuccessCode(response);

        return await response.Content.ReadFromJsonAsync<TransactionVM>();
    }

    public async Task<TransactionVM> UpdateAsync(TransactionUpdateDto transactionUpdateDto)
    {
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
}
