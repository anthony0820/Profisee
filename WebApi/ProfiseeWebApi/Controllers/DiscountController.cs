using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProfiseeWebApi.Models;
using ProfiseeWebApi.TableLogic;

namespace ProfiseeWebApi.Controllers
{
    /// <summary>
    /// API Endpoint to Handle All Interactions with the Discounts Table
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class DiscountController : Controller
    {
        public DiscountController()
        {

        }

        #region GET

        /// <summary>
        /// Retrives All Discounts from the Discounts Table
        /// </summary>
        /// <returns>Status 200 Response on Successful Get, Otherwise 400 Response</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DiscountModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllDiscounts()
        {
            //Attempt to Get All Discounts
            DiscountTableConnection discountTableConnection = new DiscountTableConnection();
            List<DiscountModel>? discounts = discountTableConnection.GetAllDiscounts();

            if (discounts != null)
            {
                //Return 200 With Discount Information on Succssful Retrieval
                return Ok(discounts);
            }
            else
            {
                //Return 400 if Error Retrieving
                return BadRequest();
            }
        }

        /// <summary>
        /// Retrives a Single Discount Based Using the Provided Discount ID
        /// </summary>
        /// <param name="discountID">The ID of the Discount to Retrieve</param>
        /// <returns>Status 200 Response on Successful Get, Otherwise 400 Response</returns>
        [HttpGet("getDiscount")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DiscountModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetDiscount(int discountID)
        {
            //Validate Input
            if (discountID <= 0)
            {
                return BadRequest();
            }

            //Attempt to Get Sale
            DiscountTableConnection discountTableConnection = new DiscountTableConnection();
            DiscountModel? discount = discountTableConnection.GetDiscounts(discountID);

            if (discount != null)
            {
                //Return 200 With Discount Information on Successful Retrieval
                return Ok(discount);
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
        /// Adds a New Discount to the Discounts Table
        /// </summary>
        /// <param name="discount">Information About the New Discount</param>
        /// <returns>200 Response on Successful Addition, Otherwise 400 Response</returns>
        [HttpPost("addDiscount")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DiscountModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddNewSale(DiscountModel discount)
        {
            //Check Received Information
            if (discount == null || !ValidateDiscount(discount))
            {
                return BadRequest();
            }

            //Attempt to Insert New Discount
            DiscountTableConnection discountTableConnection = new DiscountTableConnection();
            if (discountTableConnection.AddNewDiscount(discount))
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
        /// Updates a Discount in the Table if it Exists and Inputs are Valid
        /// </summary>
        /// <param name="discount">The Discount to Update</param>
        /// <returns>204 Response on Successful Update, Otherwise 400 Response</returns>
        [HttpPost("updateDiscount")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateDiscount(DiscountModel discount)
        {
            //Check Received Information
            if (discount == null || !ValidateDiscount(discount))
            {
                return BadRequest();
            }

            //Attempt to Update Provided Discount
            DiscountTableConnection discountTableConnection = new DiscountTableConnection();

            if (discountTableConnection.UpdateDiscount(discount))
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
        /// Deletes a Row in the Discounts Table Using the Discount ID
        /// </summary>
        /// <param name="discountID">The ID of the Discount to Delete</param>
        /// <returns>Status 204 Response on Successful Deletion, Otherwise 400 Response</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int discountID)
        {
            //Validate Input
            if (discountID <= 0)
            {
                return BadRequest();
            }
            else
            {
                //Attempt to Delete Sale
                DiscountTableConnection discountTableConnection = new DiscountTableConnection();
                if (discountTableConnection.DeleteDiscount(discountID))
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
        /// <param name="discount">The Provided Discount Information</param>
        /// <returns>True if All Fields are Valid, Otherwise False</returns>
        private bool ValidateDiscount(DiscountModel discount) 
        {
            //Ensure All IDs are Greater Than Zero
            if(discount.DiscountId > 0 && discount.ProductId > 0 && discount.Discount > 0)
            {
                //Check if All Strings are Provided
                if(!string.IsNullOrEmpty(discount.BeginDate) && !string.IsNullOrEmpty(discount.EndDate))
                {
                    return true;
                }
            }

            return true;
        }

        #endregion

    }
}
