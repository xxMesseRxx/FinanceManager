namespace FinManagerWebClient.DTO.Reports;

using FinManagerWebClient.Library;

public class PeriodReport : Report
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
