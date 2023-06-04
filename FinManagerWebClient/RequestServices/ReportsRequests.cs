namespace FinManagerWebClient.RequestServices;

using FinManagerWebClient.DTO.Reports;
using FinManagerWebClient.Library.Requests;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class ReportsRequests : IReportsRequests
{
    private readonly HttpClient _httpClient;
    private readonly string _periodReportUrl;
    private readonly string _dailyReportUrl;

    public ReportsRequests(IHttpClientFactory httpClientFactory)
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        _dailyReportUrl = config["URLs:FinanceManager:DailyReport"];
        _periodReportUrl = config["URLs:FinanceManager:PeriodReport"];

        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<DailyReport> GetDailyReportAsync(DateTime date)
    {
        return await _httpClient.GetFromJsonAsync<DailyReport>(_dailyReportUrl + "?" +
                                                               date.ToShortDateString());
    }

    public async Task<PeriodReport> GetPeriodReportAsync(DateTime startDate, DateTime endDate)
    {
        return await _httpClient.GetFromJsonAsync<PeriodReport>(_dailyReportUrl + "?" + 
                                                                $"startDate={startDate}&endDate={endDate}");
    }
}
