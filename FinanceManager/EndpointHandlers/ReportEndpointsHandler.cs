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
    public static async Task<IResult> GetPeriodReportAsync(IReportService reportService,
                                                           DateOnly startDate, DateOnly endDate)
    {
        PeriodReport periodReport = await reportService.GetPeriodReportAsync(startDate, endDate);

        return Results.Json(periodReport);
    }
}
