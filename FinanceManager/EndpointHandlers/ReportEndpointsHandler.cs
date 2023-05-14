namespace FinanceManager.EndpointHandlers;

using FinanceManager.Library.Interfaces;
using FinanceManager.Model;

public static class ReportEndpointsHandler
{
    public static async Task<IResult> GetDailyReportAsync(IReportService reportService,
                                                          DateOnly date)
    {
        DailyReport dailyReport = await reportService.GetDailyReportAsync(date);

        return Results.Json(dailyReport);
    }
}
