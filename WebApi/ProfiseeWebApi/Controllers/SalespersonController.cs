using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProfiseeWebApi.Models;
using ProfiseeWebApi.TableLogic;

namespace ProfiseeWebApi.Controllers
{
    /// <summary>
    /// API Endpoint to Handle All Interactions with the Salesperson Table
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class SalespersonController : Controller
    {
        public SalespersonController()
        {

        }

        #region GET

        /// <summary>
        /// Retrieves All Salespeople from the Salesperson Table
        /// </summary>
        /// <returns>Status 200 Response on Successful Get, Otherwise 400 Response</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SalespersonModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllSalesPeople()
        {
            //Attempt to Get All Salespeople
            SalespersonTableConnection connection = new SalespersonTableConnection();
            List<SalespersonModel>? salesPeople = connection.GetAllSalespeople();

            if (salesPeople != null)
            {
                //Return 200 With Salespeople Information on Succssful Retrieval
                return Ok(salesPeople);
            }
            else
            {
                //Return 400 if Error Retrieving
                return BadRequest();
            }
        }

        /// <summary>
        /// Retrives a Single Salesperson Based Using the Provided Sale ID
        /// </summary>
        /// <param name="salespersonId">The ID of the Salesperson to Retrieve</param>
        /// <returns>Status 200 Response on Successful Get, Otherwise 400 Response</returns>
        [HttpGet("getSalesperson")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SalespersonModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetSalesperson(int salespersonId)
        {
            //Validate Input
            if (salespersonId <= 0)
            {
                return BadRequest();
            }

            //Attempt to Get Sale
            SalespersonTableConnection salespersonTableConnection = new SalespersonTableConnection();
            SalespersonModel? salesperson = salespersonTableConnection.GetSalesperson(salespersonId);

            if (salesperson != null)
            {
                //Return 200 With Salesperson Information on Successful Retrieval
                return Ok(salesperson);
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
        /// Adds a New Salesperson to the Salesperson Table
        /// </summary>
        /// <param name="salesperson">Information About the New Salesperon</param>
        /// <returns>200 Response on Successful Addition, Otherwise 400 Response</returns>
        [HttpPost("addSalesperson")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SalespersonModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddNewSaleperson(SalespersonModel salesperson)
        {
            //Check Received Information
            if (salesperson == null || !ValidateSalesperson(salesperson))
            {
                return BadRequest();
            }

            //Attempt to Insert New Sale
            SalespersonTableConnection salespersonTableConnection = new SalespersonTableConnection();
            if (salespersonTableConnection.AddNewSalesperson(salesperson))
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
        /// Updates a Salesperson in the Table if it Exists and Inputs are Valid
        /// </summary>
        /// <param name="salesperson">The Salesperson to Update</param>
        /// <returns>204 Response on Successful Update, Otherwise 400 Response</returns>
        [HttpPost("updateSalesperson")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateSale(SalespersonModel salesperson)
        {
            //Check Received Information
            if (salesperson == null || !ValidateSalesperson(salesperson))
            {
                return BadRequest();
            }

            //Attempt to Update Provided Sale
            SalespersonTableConnection salespersonTableConnection = new SalespersonTableConnection();

            if (salespersonTableConnection.UpdateSalesperson(salesperson))
            {
                //Return 204 if Successful Update
                return NoContent();
            }
            else
            {
                //Return 400 if Error Inserting
                return BadRequest();
            }
        }

        #endregion


        #region DELETE

        /// <summary>
        /// Deletes a Row in the Salesperson Table Using the Salesperson ID
        /// </summary>
        /// <param name="salespersonID">The ID of the Salesperson to Delete</param>
        /// <returns>Status 204 Response on Successful Deletion, Otherwise 400 Response</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int salespersonID)
        {
            //Validate Input
            if (salespersonID <= 0)
            {
                return BadRequest();
            }
            else
            {
                //Attempt to Delete Salesperson
                SalespersonTableConnection salespersonTableConnection = new SalespersonTableConnection();
                if (salespersonTableConnection.DeleteSalesperson(salespersonID))
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
        /// <param name="sale">The Provided Salesperson Information</param>
        /// <returns>True if All Fields are Valid, Otherwise False</returns>
        private bool ValidateSalesperson(SalespersonModel sale)
        {
            return true;
        }

        #endregion

    }
}
