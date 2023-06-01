namespace FinManagerWebClient.RequestServices;

using FinManagerWebClient.DTO;
using FinManagerWebClient.Library.Requests;
using FinManagerWebClient.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

public class OperationRequests : IOperationRequests
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public OperationRequests(IHttpClientFactory httpClientFactory)
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        _baseUrl = config["URLs:FinanceManager:Operations"];

        _httpClient = httpClientFactory.CreateClient();
    }

    public Task CreateAsync(OperationVM operation)
    {
        throw new NotImplementedException();
    }

    public async Task<List<OperationVM>> GetAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<OperationVM>>(_baseUrl);
    }

    public Task<OperationVM> RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<OperationVM> UpdateAsync(OperationVM operation)
    {
        throw new NotImplementedException();
    }
}
