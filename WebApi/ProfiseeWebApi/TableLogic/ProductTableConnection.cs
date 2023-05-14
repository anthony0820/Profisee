using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using ProfiseeWebApi.Models;

namespace ProfiseeWebApi.TableLogic
{
    /// <summary>
    /// Handles Table Interactions for the Products Table
    /// </summary>
    public class ProductTableConnection
    {
        //Connection Information to the Database
        private const string _ConnectionString = @"Server = localhost; Database = ProfiseeTables; Trusted_Connection = True; TrustServerCertificate=True";

        /// <summary>
        /// Handles Table Interactions for the Products Table
        /// </summary>
        public ProductTableConnection()
        {

        }

        #region GET

        /// <summary>
        /// Retrieves All Products from the Products Table
        /// </summary>
        /// <returns>A List of All Products if Successful, Otherwise Null</returns>
        public List<ProductsModel>? GetAllProducts()
        {
            List<ProductsModel> products = new List<ProductsModel>();

            try
            {
                //sql connection object
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {

                    //retrieve the SQL Server instance version
                    string query = @"SELECT * FROM dbo.Products";


                    //define the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, conn);

                    //open connection
                    conn.Open();

                    //execute the SQLCommand
                    SqlDataReader dr = cmd.ExecuteReader();


                    //check if there are records
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ProductsModel product = new ProductsModel()
                            {
                                ProductId = dr.GetInt32(0),
                                Name = dr.GetString(1),
                                Manufacturer = dr.GetString(2),
                                Style = dr.GetString(3),
                                PurchasePrice = dr.GetDouble(4),
                                SalePrice = dr.GetDouble(5),
                                QtyOnHand = dr.GetInt32(6),
                                CommissionPercentage = dr.GetDouble(7)
                            };
                            products.Add(product);
                        }
                    }

                    //close data reader
                    dr.Close();

                    //close connection
                    conn.Close();

                    return products;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Attempts to Retrieve a Product Based on the Product ID
        /// </summary>
        /// <param name="productID">The ID of the Product to Retrieve</param>
        /// <returns>The Product Information if Found, Otherwise False</returns>
        public ProductsModel? GetProduct(int productID)
        {
            ProductsModel? product = null;
            try
            {
                //sql connection object
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {

                    //retrieve the SQL Server instance version
                    string query = $"SELECT * FROM dbo.Products WHERE ProductId = {productID}";


                    //define the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, conn);

                    //open connection
                    conn.Open();

                    //execute the SQLCommand
                    SqlDataReader dr = cmd.ExecuteReader();


                    //check if there are records
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            product = new ProductsModel()
                            {
                                ProductId = dr.GetInt32(0),
                                Name = dr.GetString(1),
                                Manufacturer = dr.GetString(2),
                                Style = dr.GetString(3),
                                PurchasePrice = dr.GetDouble(4),
                                SalePrice = dr.GetDouble(5),
                                QtyOnHand = dr.GetInt32(6),
                                CommissionPercentage = dr.GetDouble(7)
                            };
                        }
                    }

                    //close data reader
                    dr.Close();

                    //close connection
                    conn.Close();

                    return product;

                }
            }
            catch(Exception)
            {
                return null;
            }
        }

        #endregion

        #region POST

        /// <summary>
        /// Attempts to Add a New Product to the Products Table
        /// </summary>
        /// <param name="sale">The Product Information to Add to the Table</param>
        /// <returns>True if Product Successfully Added, Otherwise False</returns>
        public bool AddNewProduct(ProductsModel product)
        {
            try
            {
                //create the SqlConnection object
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {

                    //retrieve the SQL Server instance version
                    string query = @"INSERT INTO dbo.Products (ProductId, Name, Manufacturer, Style, PurchasePrice, SalePrice, QtyOnHand, CommissionPercentage)";
                    query += " VALUES (@ProductId, @Name, @Manufacturer, @Style, @PurchasePrice, @SalePrice, @QtyOnHand, @CommissionPercentage)";
                    //create the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                    cmd.Parameters.AddWithValue("@Name", product.Name);
                    cmd.Parameters.AddWithValue("@Manufacturer", product.Manufacturer);
                    cmd.Parameters.AddWithValue("@Style", product.Style);
                    cmd.Parameters.AddWithValue("@PurchasePrice", product.PurchasePrice);
                    cmd.Parameters.AddWithValue("@SalePrice", product.SalePrice);
                    cmd.Parameters.AddWithValue("@QtyOnHand", product.QtyOnHand);
                    cmd.Parameters.AddWithValue("@CommissionPercentage", product.CommissionPercentage);

                    //open connection
                    conn.Open();

                    //execute the SQL Command (UPDATE)
                    cmd.ExecuteNonQuery();

                    //close connection
                    conn.Close();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Attempts to Update a Product Row in the Products Table
        /// </summary>
        /// <param name="sale">The Product Information to Update</param>
        /// <returns>True if Product Updated Successfully, Otherwise False</returns>
        public bool UpdateProduct(ProductsModel product)
        {
            try
            {
                //create the SqlConnection object
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {

                    //retrieve the SQL Server instance version
                    string query = @"UPDATE dbo.Products SET Name = @Name, Manufacturer = @Manufacturer, Style = @Style, PurchasePrice = @PurchasePrice, SalePrice = @SalePrice, QtyOnHand = @QtyOnHand, CommissionPercentage = @CommissionPercentage WHERE ProductId = @ProductId;";
                    //create the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                    cmd.Parameters.AddWithValue("@Name", product.Name);
                    cmd.Parameters.AddWithValue("@Manufacturer", product.Manufacturer);
                    cmd.Parameters.AddWithValue("@Style", product.Style);
                    cmd.Parameters.AddWithValue("@PurchasePrice", product.PurchasePrice);
                    cmd.Parameters.AddWithValue("@SalePrice", product.SalePrice);
                    cmd.Parameters.AddWithValue("@QtyOnHand", product.QtyOnHand);
                    cmd.Parameters.AddWithValue("@CommissionPercentage", product.CommissionPercentage);

                    //open connection
                    conn.Open();

                    //execute the SQL Command (UPDATE)
                    cmd.ExecuteNonQuery();

                    //close connection
                    conn.Close();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region DELETE

        /// <summary>
        /// Attempts to Delete a Product from the Table Using the Product ID
        /// </summary>
        /// <param name="productID">The ID of the Product to Delete</param>
        /// <returns>True if Product Deleted Successfully, Otherwise False</returns>
        public bool DeleteProduct(int productID)
        {
            try
            {
                //create the SqlConnection object
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {

                    //retrieve the SQL Server instance version
                    string query = @"DELETE FROM dbo.Products WHERE ProductId = @productId";

                    //create the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@productId", productID);

                    //open connection
                    conn.Open();

                    //execute the SQL Command (UPDATE)
                    cmd.ExecuteNonQuery();

                    //close connection
                    conn.Close();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}
