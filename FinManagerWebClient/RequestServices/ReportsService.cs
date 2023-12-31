﻿namespace FinManagerWebClient.RequestServices;

using FinManagerWebClient.DTO.Reports;
using FinManagerWebClient.Library.Requests;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class ReportsService : IReportsService
{
    private readonly HttpClient _httpClient;
    private readonly string _periodReportUrl;
    private readonly string _dailyReportUrl;

    public ReportsService(IHttpClientFactory httpClientFactory)
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
        return await _httpClient.GetFromJsonAsync<DailyReport>($"{_dailyReportUrl}?date={date.ToString("yyyy-MM-dd")}");
    }

    public async Task<PeriodReport> GetPeriodReportAsync(DateTime startDate, DateTime endDate)
    {
        CheckDates(startDate, endDate);

        return await _httpClient.GetFromJsonAsync<PeriodReport>($"{_periodReportUrl}" + 
                                                                $"?startDate={startDate.ToString("yyyy-MM-dd")}" +
                                                                $"&endDate={endDate.ToString("yyyy-MM-dd")}");
    }

    private void CheckDates(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
        {
            throw new ArgumentException("Start date cannot be bigger than end date");
        }
    }
}
