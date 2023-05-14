namespace ProfiseeWebApi.Models
{
    /// <summary>
    /// Models the Products Table Values
    /// </summary>
    public class ProductsModel
    {
        /// <summary>
        /// The ID of the Product
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// The Name of the Product
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// The Name of the Manufacturer That Made the Product
        /// </summary>
        public string? Manufacturer { get; set; }
        /// <summary>
        /// Type of Bike
        /// </summary>
        public string? Style { get; set; }
        /// <summary>
        /// Wholesale Price of the Bike in Dollars
        /// </summary>
        public double PurchasePrice { get; set; }
        /// <summary>
        /// Sale Price of the Bike in Dollars
        /// </summary>
        public double SalePrice { get; set; }
        /// <summary>
        /// Number of Bikes in Stock
        /// </summary>
        public int QtyOnHand { get; set; }
        /// <summary>
        /// Amount of Commission Made for Sale
        /// </summary>
        public double CommissionPercentage { get; set; }

    }
}
