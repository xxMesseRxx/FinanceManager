namespace FinanceManager.Controllers;

using FinanceManager.DAL;
using FinanceManager.DAL.DTO.Reports;
using FinanceManager.Library.Interfaces;
using FinanceManager.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]/[action]")]
[ApiController]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("{date}")]
    public async Task<DailyReport> DailyReportAsync(DateTime date)
    {
        return await _reportService.GetDailyReportAsync(DateOnly.FromDateTime(date));
    }

    [HttpGet("{startDate}-{endDate}")]
    public async Task<PeriodReport> PeriodReportAsync(DateTime startDate, DateTime endDate)
    {
        return await _reportService.GetPeriodReportAsync(DateOnly.FromDateTime(startDate),
                                                         DateOnly.FromDateTime(endDate));
    }
}
