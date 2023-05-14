using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProfiseeWebApi.Models;
using ProfiseeWebApi.TableLogic;

namespace ProfiseeWebApi.Controllers
{
    /// <summary>
    /// API Endpoint to Handle All Interactions with the Customers Table
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        public CustomerController()
        {

        }

        #region GET

        /// <summary>
        /// Retrieves All Customers from the Customers Table
        /// </summary>
        /// <returns>Status 200 Response on Successful Get, Otherwise 400 Response</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CustomerModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllCustomers()
        {
            //Attempt to Get All Customers
            CustomerTableConnection customerTableConnection = new CustomerTableConnection();
            List<CustomerModel>? customers = customerTableConnection.GetAllCustomers();

            if (customers != null)
            {
                //Return 200 With Sales Information on Succssful Retrieval
                return Ok(customers);
            }
            else
            {
                //Return 400 if Error Retrieving
                return BadRequest();
            }
        }

        /// <summary>
        /// Retrives a Single Customer Based Using the Provided Customer ID
        /// </summary>
        /// <param name="customerId">The ID of the Customer to Retrieve</param>
        /// <returns>Status 200 Response on Successful Get, Otherwise 400 Response</returns>
        [HttpGet("getCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SalesModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetCustomer(int customerId)
        {
            //Validate Input
            if (customerId <= 0)
            {
                return BadRequest();
            }

            //Attempt to Get Sale
            CustomerTableConnection customerTableConnection = new CustomerTableConnection();
            CustomerModel? customer = customerTableConnection.GetCustomer(customerId);

            if (customer != null)
            {
                //Return 200 With Sale Information on Successful Retrieval
                return Ok(customer);
            }
            else
            {
                //Return 400 if Error Retrieving
                return BadRequest();
            }
        }

        #endregion

        #region POST

        /// <summary>
        /// Adds a New Customer to the CustomerS Table
        /// </summary>
        /// <param name="customer">Information About the New Customer</param>
        /// <returns>200 Response on Successful Addition, Otherwise 400 Response</returns>
        [HttpPost("addCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddNewCustomer(CustomerModel customer)
        {
            //Check Received Information
            if (customer == null || !ValidateCustomer(customer))
            {
                return BadRequest();
            }

            //Attempt to Insert New Customer
            CustomerTableConnection customerTableConnection = new CustomerTableConnection();
            if (customerTableConnection.AddNewCustomer(customer))
            {
                //Return 204 if Successful Insertion
                return NoContent();
            }
            else
            {
                //Return 400 if Error Inserting
                return BadRequest();
            }
        }

        /// <summary>
        /// Updates a Customer in the Table if it Exists and Inputs are Valid
        /// </summary>
        /// <param name="customer">The Customer to Update</param>
        /// <returns>204 Response on Successful Update, Otherwise 400 Response</returns>
        [HttpPost("updateCustomer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateCustomer(CustomerModel customer)
        {
            //Check Received Information
            if (customer == null || !ValidateCustomer(customer))
            {
                return BadRequest("Could NOT Validate Customer");
            }

            //Attempt to Update Provided Sale
            CustomerTableConnection customerTableConnection = new CustomerTableConnection();

            if (customerTableConnection.UpdateCustomer(customer))
            {
                //Return 204 if Successful Update
                return NoContent();
            }
            else
            {
                //Return 400 if Error Inserting
                return BadRequest("Could NOT Update Customer");
            }
        }

        #endregion


        #region DELETE

        /// <summary>
        /// Deletes a Row in the Customers Table Using the Customer ID
        /// </summary>
        /// <param name="customerID">The ID of the Customer to Delete</param>
        /// <returns>Status 204 Response on Successful Deletion, Otherwise 400 Response</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int customerID)
        {
            //Validate Input
            if (customerID <= 0)
            {
                return BadRequest();
            }
            else
            {
                //Attempt to Delete Sale
                CustomerTableConnection customerTableConnection = new CustomerTableConnection();
                if (customerTableConnection.DeleteCustomer(customerID))
                {
                    //Return 204 on Successful Deletion
                    return NoContent();
                }
                else
                {
                    //Return 400 on Error Deleting
                    return BadRequest();
                }
            }
        }

        #endregion

        #region HELP METHODS

        /// <summary>
        /// Checks if All Values Provided are Valid Inputs
        /// </summary>
        /// <param name="customer">The Provided Customer Information</param>
        /// <returns>True if All Fields are Valid, Otherwise False</returns>
        private bool ValidateCustomer(CustomerModel customer)
        {
            //Do NOT Allow Negative Number for ID
            if(customer.CustomerId > 0)
            {
                //Ensure All String Values are Set
                if(!string.IsNullOrEmpty(customer.FirstName) && !string.IsNullOrEmpty(customer.LastName) && !string.IsNullOrEmpty(customer.Address)
                    && !string.IsNullOrEmpty(customer.Phone) && !string.IsNullOrEmpty(customer.StartDate))
                {
                    return true;
                }
            }

            //Return False if Any Field NOT Valid
            return false;
        }

        #endregion



    }
}
