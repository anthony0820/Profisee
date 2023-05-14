namespace ProfiseeWebApi.Models
{
    /// <summary>
    /// Models the Sales Table Values
    /// </summary>
    public class SalesModel
    {
        /// <summary>
        /// The ID of the Sale
        /// </summary>
        public int SalesId { get;set; }
        /// <summary>
        /// The ID of the Product for the Sale
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// The ID of the Customer Who Bought the Product
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// The ID of the Salesperson Who Sold the Product
        /// </summary>
        public int SalespersonId { get; set; }
        /// <summary>
        /// The Date of the Sale
        /// </summary>
        public string? SalesDate { get; set; }

    }
}
