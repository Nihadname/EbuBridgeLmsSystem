namespace EbuBridgeLmsSystem.Application.Dtos.Report
{
    public record ReportReturnDto
    {
        public string Description { get; init; }
        public UserReportReturnDto userReportReturnDto { get; init; }
        public ReportOptionInReportReturnDto optionInReportReturnDto { get; init; }
    }
}