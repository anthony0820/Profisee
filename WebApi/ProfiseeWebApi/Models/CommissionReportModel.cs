namespace ProfiseeWebApi.Models
{
    /// <summary>
    /// Models a Quarterly Commission Report for Sales People
    /// </summary>
    public class CommissionReportModel
    {
        /// <summary>
        /// The ID for the Commission Report Row
        /// </summary>
        public int ReportId { get; set; }
        /// <summary>
        /// The Sales person who Made Commission
        /// </summary>
        public string? Salesperson { get; set; }
        /// <summary>
        /// The Total Amount of Commission Made in Dollars
        /// </summary>
        public double Commission { get; set; }
    }
}
