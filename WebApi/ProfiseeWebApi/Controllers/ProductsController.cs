using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProfiseeWebApi.Models;
using ProfiseeWebApi.TableLogic;

namespace ProfiseeWebApi.Controllers
{
    /// <summary>
    /// API Endpoint to Handle All Interactions with the Products Table
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        public ProductsController()
        {

        }

        #region GET

        /// <summary>
        /// Retrieves All Products from the Products Table
        /// </summary>
        /// <returns>Status 200 Response on Successful Get, Otherwise 400 Response</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductsModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllProducts()
        {
            //Attempt to Get All Products
            ProductTableConnection productTableConnection = new ProductTableConnection();
            List<ProductsModel>? products = productTableConnection.GetAllProducts();

            if(products != null)
            {
                //Return 200 With Product Information on Succssful Retrieval
                return Ok(products);
            }
            else
            {
                //Return 400 if Error Retrieving
                return BadRequest();
            }
        }

        /// <summary>
        /// Retrives a Single Based Using the Provided Sale ID
        /// </summary>
        /// <param name="productID">The ID of the Sale to Retrieve</param>
        /// <returns>Status 200 Response on Successful Get, Otherwise 400 Response</returns>
        [HttpGet("getProduct")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductsModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetProduct(int productID)
        {
            //Validate Input
            if (productID <= 0)
            {
                return BadRequest();
            }

            //Attempt to Get Product
            ProductTableConnection productTableConnection = new ProductTableConnection();
            ProductsModel? product = productTableConnection.GetProduct(productID);

            if (product != null)
            {
                //Return 200 With Product Information on Successful Retrieval
                return Ok(product);
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
        /// Adds a New Product to the Products Table
        /// </summary>
        /// <param name="product">Information About the New Product</param>
        /// <returns>200 Response on Successful Addition, Otherwise 400 Response</returns>
        [HttpPost("addProduct")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductsModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddNewProduct(ProductsModel product)
        {
            //Check Received Information
            if (product == null || !ValideProduct(product))
            {
                return BadRequest();
            }

            //Attempt to Insert New Sale
            ProductTableConnection productTableConnection = new ProductTableConnection();
            if (productTableConnection.AddNewProduct(product))
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
        /// Updates a Product in the Table if it Exists and Inputs are Valid
        /// </summary>
        /// <param name="product">The Product to Update</param>
        /// <returns>204 Response on Successful Update, Otherwise 400 Response</returns>
        [HttpPost("updateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateProduct(ProductsModel product)
        {
            //Check Received Information
            if (product == null || !ValideProduct(product))
            {
                return BadRequest();
            }

            //Attempt to Update Provided Product
            ProductTableConnection productsTableConnection = new ProductTableConnection();

            if (productsTableConnection.UpdateProduct(product))
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
        /// Deletes a Row in the Products Table Using the Product ID
        /// </summary>
        /// <param name="productID">The ID of the Product to Delete</param>
        /// <returns>Status 204 Response on Successful Deletion, Otherwise 400 Response</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int productID)
        {
            //Validate Input
            if (productID <= 0)
            {
                return BadRequest();
            }
            else
            {
                //Attempt to Delete Sale
                ProductTableConnection productsTableConnection = new ProductTableConnection();
                if (productsTableConnection.DeleteProduct(productID))
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
        /// <param name="product">The Provided Product Information</param>
        /// <returns>True if All Fields are Valid, Otherwise False</returns>
        private bool ValideProduct(ProductsModel product)
        {
            //Validate Number Values
            if(product.ProductId > 0 && product.PurchasePrice > 0 && product.SalePrice > 0 && product.QtyOnHand >= 0 && product.CommissionPercentage >= 0)
            {
                //Validate String Values
                if(!string.IsNullOrEmpty(product.Name) && !string.IsNullOrEmpty(product.Manufacturer) && !string.IsNullOrEmpty(product.Style))
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
