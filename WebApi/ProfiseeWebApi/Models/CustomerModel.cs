namespace ProfiseeWebApi.Models
{
    /// <summary>
    /// Models a Customer of BeSpoked Bikes
    /// </summary>
    public class CustomerModel
    {
        /// <summary>
        /// The ID of the BeSpoked Bikes Customer
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// The First Name of the BeSpoked Bikes Customer
        /// </summary>
        public string? FirstName { get; set; }
        /// <summary>
        /// The Last Name of the BeSpoked Bikes Customer
        /// </summary>
        public string? LastName { get; set; }
        /// <summary>
        /// The Address of the BeSpoked Bikes Customer
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        /// The Phone Number of the BeSpoked Bikes Customer
        /// </summary>
        public string? Phone { get; set; }
        /// <summary>
        /// The Date of the Sale for the BeSpoked Bikes Customer
        /// </summary>
        public string? StartDate { get; set; }
    }
}
