namespace ProfiseeWebApi.Models
{
    /// <summary>
    /// Models the Discounts Table Values
    /// </summary>
    public class DiscountModel
    {
        /// <summary>
        /// The ID of the Discount
        /// </summary>
        public int DiscountId { get; set; }
        /// <summary>
        /// The ID fo the Product with the Discount
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// The Starting Date for the Discount
        /// </summary>
        public string? BeginDate { get; set; }
        /// <summary>
        /// The End Date for the Discount
        /// </summary>
        public string? EndDate { get; set; }
        /// <summary>
        /// The Percentage of the Discount as a Decimal
        /// </summary>
        public double Discount { get; set; }
    }
}
