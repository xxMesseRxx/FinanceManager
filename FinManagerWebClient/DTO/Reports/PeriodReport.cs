namespace FinManagerWebClient.DTO.Reports;

using FinManagerWebClient.Library;

public class PeriodReport : Report
{
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}
