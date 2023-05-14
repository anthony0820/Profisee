using Microsoft.Data.SqlClient;
using ProfiseeWebApi.Models;

namespace ProfiseeWebApi.TableLogic
{
    /// <summary>
    /// Handles Table Interactions for the Discount Table
    /// </summary>
    public class DiscountTableConnection
    {
        //Connection Information to the Database
        private const string _ConnectionString = @"Server = localhost; Database = ProfiseeTables; Trusted_Connection = True; TrustServerCertificate=True";

        /// <summary>
        /// Handles Table Interactions for the Discounts Table
        /// </summary>
        public DiscountTableConnection()
        {

        }

        #region GET

        /// <summary>
        /// Retrieves All Discounts in the Discounts Table
        /// </summary>
        /// <returns>A List of all Discounts if Successful, Otherwise Null</returns>
        public List<DiscountModel>? GetAllDiscounts()
        {   
            List<DiscountModel> discounts = new List<DiscountModel>();

            try
            {
                //sql connection object
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {

                    //retrieve the SQL Server instance version
                    string query = @"SELECT * FROM dbo.Discount";


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
                            DiscountModel discount = new DiscountModel()
                            {
                                DiscountId = dr.GetInt32(0),
                                ProductId = dr.GetInt32(1),
                                BeginDate = dr.GetString(2),
                                EndDate = dr.GetString(3),
                                Discount = dr.GetDouble(4)
                            };
                            discounts.Add(discount);
                        }
                    }

                    //close data reader
                    dr.Close();

                    //close connection
                    conn.Close();

                    return discounts;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Attempts to Retrieve a Discount Based on the Discount ID
        /// </summary>
        /// <param name="discountID">The ID of the Discount to Retrieve</param>
        /// <returns>The Discount Information if Found, Otherwise False</returns>
        public DiscountModel? GetDiscounts(int discountID)
        {
            DiscountModel? discount = null;
            try
            {
                //sql connection object
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {

                    //retrieve the SQL Server instance version
                    string query = $"SELECT * FROM dbo.Discount WHERE DiscountId = {discountID}";


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
                            discount = new DiscountModel()
                            {
                                DiscountId = dr.GetInt32(0),
                                ProductId = dr.GetInt32(1),
                                BeginDate = dr.GetString(2),
                                EndDate = dr.GetString(3),
                                Discount = dr.GetDouble(4)
                            };
                        }
                    }

                    //close data reader
                    dr.Close();

                    //close connection
                    conn.Close();

                    return discount;

                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region POST

        /// <summary>
        /// Attempts to Add a New Discount to the Discounts Table
        /// </summary>
        /// <param name="discount">The Discount Information to Add to the Table</param>
        /// <returns>True if Sale Successfully Added, Otherwise False</returns>
        public bool AddNewDiscount(DiscountModel discount)
        {
            try
            {
                //create the SqlConnection object
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    //retrieve the SQL Server instance version
                    string query = @"INSERT INTO dbo.Discount (DiscountId, ProductId, BeginDate, EndDate, DiscountPercentage)";
                    query += " VALUES (@DiscountId, @ProductId, @BeginDate, @EndDate, @DiscountPercentage)";
                    //create the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@DiscountId", discount.DiscountId);
                    cmd.Parameters.AddWithValue("@ProductId", discount.ProductId);
                    cmd.Parameters.AddWithValue("@BeginDate", discount.BeginDate);
                    cmd.Parameters.AddWithValue("@EndDate", discount.EndDate);
                    cmd.Parameters.AddWithValue("@DiscountPercentage", discount.Discount);

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
        /// Attempts to Update a Discount Row in the Discounts Table
        /// </summary>
        /// <param name="discount">The Discount Information to Update</param>
        /// <returns>True if Discount Updated Successfully, Otherwise False</returns>
        public bool UpdateDiscount(DiscountModel discount)
        {
            try
            {
                //create the SqlConnection object
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {

                    //retrieve the SQL Server instance version
                    string query = @"UPDATE dbo.Discount SET ProductId = @productId, BeginDate = @BeginDate, EndDate = @EndDate, DiscountPercentage = @DiscountPercentage WHERE DiscountId = @DiscountId;";
                    //create the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@DiscountId", discount.DiscountId);
                    cmd.Parameters.AddWithValue("@ProductId", discount.ProductId);
                    cmd.Parameters.AddWithValue("@BeginDate", discount.BeginDate);
                    cmd.Parameters.AddWithValue("@EndDate", discount.EndDate);
                    cmd.Parameters.AddWithValue("@DiscountPercentage", discount.Discount);

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
        /// Attempts to Delete a Discount from the Table Using the Discount ID
        /// </summary>
        /// <param name="discountID">The ID of the Discount to Delete</param>
        /// <returns>True if Sale Deleted Successfully, Otherwise False</returns>
        public bool DeleteDiscount(int discountID)
        {
            try
            {
                //create the SqlConnection object
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {

                    //retrieve the SQL Server instance version
                    string query = @"DELETE FROM dbo.Discount WHERE DiscountId = @discountId";

                    //create the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@discountId", discountID);

                    //open connection
                    conn.Open();

                    //execute the SQL Command (DELETE)
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
