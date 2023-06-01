namespace FinManagerWebClient.RequestServices;

using FinManagerWebClient.DTO;
using FinManagerWebClient.Library.Requests;
using FinManagerWebClient.Model;
using System.Collections.Generic;
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

    public async Task CreateAsync(TransactionVM transaction)
    {
        await _httpClient.PostAsJsonAsync<TransactionVM>(_baseUrl, transaction);
    }

    public async Task<List<TransactionVM>> GetAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<TransactionVM>>(_baseUrl);
    }

    public Task<TransactionVM> RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<TransactionVM> UpdateAsync(TransactionUpdateDto transactionUpdateDto)
    {
        throw new NotImplementedException();
    }
}
