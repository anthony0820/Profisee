using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProfiseeWebApi.Models;
using ProfiseeWebApi.TableLogic;

namespace ProfiseeWebApi.Controllers
{
    /// <summary>
    /// API Endpoint to Handle All Interactions with the Sales Table
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class SalesController : Controller
    {
        public SalesController()
        {

        }

        #region GET

        /// <summary>
        /// Retrives All Sales from the Sales Table
        /// </summary>
        /// <returns>Status 200 Response on Successful Get, Otherwise 400 Response</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReadableSalesModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllSales()
        {
            //Attempt to Get All Sales
            SalesTableConnection salesTableConnection = new SalesTableConnection();
            List<ReadableSalesModel>? sales = salesTableConnection.GetAllSales();

            if (sales != null)
            {
                //Return 200 With Sales Information on Succssful Retrieval
                return Ok(sales);
            }
            else
            {
                //Return 400 if Error Retrieving
                return BadRequest();
            }
        }

        /// <summary>
        /// Retrives a Single Sale Based Using the Provided Sale ID
        /// </summary>
        /// <param name="saleId">The ID of the Sale to Retrieve</param>
        /// <returns>Status 200 Response on Successful Get, Otherwise 400 Response</returns>
        [HttpGet("getSale")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SalesModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetSale(int saleId)
        {
            //Validate Input
            if (saleId <= 0)
            {
                return BadRequest();
            }

            //Attempt to Get Sale
            SalesTableConnection salesTableConnection = new SalesTableConnection();
            SalesModel? sale = salesTableConnection.GetSale(saleId);

            if (sale != null)
            {
                //Return 200 With Sale Information on Successful Retrieval
                return Ok(sale);
            }
            else
            {
                //Return 400 if Error Retrieving
                return BadRequest();
            }
        }

        /// <summary>
        /// Retrieves the Sales Report for the Last Quarter
        /// </summary>
        /// <returns>Status 200 Response on Successful Get, Otherwise 400 Response</returns>
        [HttpGet("getSalesReport")]
        public IActionResult GetSalesReport()
        {
            //Attempt to Get Sale
            SalesTableConnection salesTableConnection = new SalesTableConnection();
            List<CommissionReportModel> commmissionReport = salesTableConnection.GetCommissionReport();

            if (commmissionReport != null)
            {
                //Return 200 With Sale Information on Successful Retrieval
                return Ok(commmissionReport);
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
        /// Adds a New Human Readable Sale to the Sales Table
        /// </summary>
        /// <param name="sale">Information About the New Sale</param>
        /// <returns>200 Response on Successful Addition, Otherwise 400 Response</returns>
        [HttpPost("addReadableSale")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(AddSaleModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddHumanReadableSale(AddSaleModel sale)
        {
            //Check Received Information
            if(sale == null)
            {
                return BadRequest();
            }

            //Attempt to Insert New Sale
            SalesTableConnection salesTableConnection = new SalesTableConnection();
            if(salesTableConnection.AddHumanReadableSale(sale))
            {
                //Return 204 if Successful Addition
                return NoContent();
            }
            else
            {
                //Return 400 if Error Inserting
                return BadRequest();
            }
        }


        /// <summary>
        /// Adds a New Sale to the Sales Table
        /// </summary>
        /// <param name="sale">Information About the New Sale</param>
        /// <returns>200 Response on Successful Addition, Otherwise 400 Response</returns>
        [HttpPost("addSale")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SalesModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddNewSale(SalesModel sale)
        {
            //Check Received Information
            if (sale == null || !ValidateSale(sale))
            {
                return BadRequest();
            }

            //Attempt to Insert New Sale
            SalesTableConnection salesTableConnection = new SalesTableConnection();
            if (salesTableConnection.AddNewSale(sale))
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
        /// Updates a Sale in the Table if it Exists and Inputs are Valid
        /// </summary>
        /// <param name="sale">The Sale to Update</param>
        /// <returns>204 Response on Successful Update, Otherwise 400 Response</returns>
        [HttpPost("updateSale")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateSale(SalesModel sale)
        {
            //Check Received Information
            if (sale == null || !ValidateSale(sale))
            {
                return BadRequest();
            }

            //Attempt to Update Provided Sale
            SalesTableConnection salesTableConnection = new SalesTableConnection();

            if (salesTableConnection.UpdateSale(sale))
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
        /// Deletes a Row in the Sales Table Using the Sale ID
        /// </summary>
        /// <param name="saleID">The ID of the Sale to Delete</param>
        /// <returns>Status 204 Response on Successful Deletion, Otherwise 400 Response</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int saleID)
        {
            //Validate Input
            if (saleID <= 0)
            {
                return BadRequest();
            }
            else
            {
                //Attempt to Delete Sale
                SalesTableConnection salesTableConnection = new SalesTableConnection();
                if (salesTableConnection.DeleteSale(saleID))
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
        /// <param name="sale">The Provided Sale Information</param>
        /// <returns>True if All Fields are Valid, Otherwise False</returns>
        private bool ValidateSale(SalesModel sale)
        {
            //Do NOT Allow Negative Numbers for the IDs
            if (sale.SalesId > 0 && sale.ProductId > 0 && sale.CustomerId > 0 && sale.SalespersonId > 0)
            {
                //Check if Provided Date Exists
                if (!string.IsNullOrEmpty(sale.SalesDate))
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
