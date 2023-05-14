namespace ProfiseeWebApi.Models
{
    /// <summary>
    /// Models Human Readable Information About a Sale
    /// </summary>
    public class ReadableSalesModel
    {
        /// <summary>
        /// The ID of the Sale
        /// </summary>
        public int SaleId { get; set; }
        /// <summary>
        /// The Name of the Product Sold
        /// </summary>
        public string? Product { get; set; }
        /// <summary>
        /// The Name of the Customer Who Bought the Product
        /// </summary>
        public string? Customer { get; set; }
        /// <summary>
        /// The Date of the Sale
        /// </summary>
        public string? Date { get; set; }
        /// <summary>
        /// The Price of the Sale
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// The Person Who Sold the Product
        /// </summary>
        public string? Salesperson { get; set; }
        /// <summary>
        /// The Amount of Commission Made on the Sale
        /// </summary>
        public double Commission { get; set; }
    }
}
