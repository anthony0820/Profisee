namespace ProfiseeWebApi.Models
{
    /// <summary>
    /// Models a Sales Person for BeSpoked Bikes
    /// </summary>
    public class SalespersonModel
    {
        public int SalespersonID { get; set; }
        /// <summary>
        /// The First Name of the Sales Person
        /// </summary>
        public string? FirstName { get; set; }
        /// <summary>
        /// The Last Name of the Sales Person
        /// </summary>
        public string? LastName { get; set; }
        /// <summary>
        /// The Address of the Sales Person
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        /// The Phone Number of the Sales Person in Format (XXX)-XXX-XXXX
        /// </summary>
        public string? Phone { get; set; }
        /// <summary>
        /// The Beginning Date of the Sales Person's Contract
        /// </summary>
        public string? StartDate { get; set; }
        /// <summary>
        /// The Ending Date of the Sales Person's Contract
        /// </summary>
        public string? TerminationDate { get; set; }
        /// <summary>
        /// The Salesperson Who Manages this Sales Person
        /// </summary>
        public int Manager { get; set; }
    }
}
