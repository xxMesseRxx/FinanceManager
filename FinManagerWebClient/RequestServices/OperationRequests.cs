namespace FinManagerWebClient.RequestServices;

using FinManagerWebClient.DTO.Operation;
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

    public async Task CreateAsync(OperationCreateDto operationCreateDto)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync<OperationCreateDto>(_baseUrl, operationCreateDto);

        await CheckSuccessCode(response);
    }

    public async Task<List<OperationVM>> GetAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<OperationVM>>(_baseUrl);
    }

    public async Task<OperationVM> RemoveAsync(int id)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync(_baseUrl +  "/" + id);

        await CheckSuccessCode(response);

        return await response.Content.ReadFromJsonAsync<OperationVM>();
    }

    public async Task<OperationVM> UpdateAsync(OperationUpdateDto operationUpdateDto)
    {
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
}
