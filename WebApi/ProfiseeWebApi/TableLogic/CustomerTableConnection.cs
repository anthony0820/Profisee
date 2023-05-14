using Microsoft.Data.SqlClient;
using ProfiseeWebApi.Models;

namespace ProfiseeWebApi.TableLogic
{
    /// <summary>
    /// Handls Table Interactions for the Customers Table
    /// </summary>
    public class CustomerTableConnection
    {
        //Connection Information to the Database
        private const string _ConnectionString = @"Server = localhost; Database = ProfiseeTables; Trusted_Connection = True; TrustServerCertificate=True";

        /// <summary>
        /// Handles Table Interactions for the Customers Table
        /// </summary>
        public CustomerTableConnection()
        {

        }

        #region GET

        /// <summary>
        /// Retrieves All Customers in the Customers Table
        /// </summary>
        /// <returns>A List of All Customers if Successful, Otherwise Null</returns>
        public List<CustomerModel>? GetAllCustomers()
        {
            List<CustomerModel> customers = new List<CustomerModel>();

            try
            {
                //sql connection object
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {

                    //retrieve the SQL Server instance version
                    string query = @"SELECT * FROM dbo.Customer";


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
                            CustomerModel customer = new CustomerModel()
                            {
                                CustomerId = dr.GetInt32(0),
                                FirstName = dr.GetString(1),
                                LastName = dr.GetString(2),
                                Address = dr.GetString(3),
                                Phone = dr.GetString(4),
                                StartDate = dr.GetString(5)
                            };
                            customers.Add(customer);
                        }
                    }

                    //close data reader
                    dr.Close();

                    //close connection
                    conn.Close();

                    return customers;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Attempts to Retrieve a Customer Based on the Customer ID
        /// </summary>
        /// <param name="customerID">The ID of the Customer to Retrieve</param>
        /// <returns>The Customer Information if Found, Otherwise False</returns>
        public CustomerModel? GetCustomer(int customerID)
        {
            CustomerModel? customer = null;
            try
            {
                //sql connection object
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {

                    //retrieve the SQL Server instance version
                    string query = $"SELECT * FROM dbo.Customer WHERE CustomerId = {customerID}";


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
                            customer = new CustomerModel()
                            {
                                CustomerId = dr.GetInt32(0),
                                FirstName = dr.GetString(1),
                                LastName = dr.GetString(2),
                                Address = dr.GetString(3),
                                Phone = dr.GetString(4),
                                StartDate = dr.GetString(5)
                            };
                        }
                    }

                    //close data reader
                    dr.Close();

                    //close connection
                    conn.Close();

                    return customer;

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
        /// Attempts to Add a New Customer to the Customer Table
        /// </summary>
        /// <param name="customer">The Customer Information to Add to the Table</param>
        /// <returns>True if Customer Successfully Added, Otherwise False</returns>
        public bool AddNewCustomer(CustomerModel customer)
        {
            try
            {
                //create the SqlConnection object
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {

                    //retrieve the SQL Server instance version
                    string query = @"INSERT INTO dbo.Customer (CustomerId, FirstName, LastName, Address, Phone, StartDate)";
                    query += " VALUES (@CustomerId, @FirstName, @LastName, @Address, @Phone, @StartDate)";
                    //create the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                    cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@Address", customer.Address);
                    cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                    cmd.Parameters.AddWithValue("@StartDate", customer.StartDate);

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
        /// Attempts to Update a Customer Row in the Customer Table
        /// </summary>
        /// <param name="customer">The Customer Information to Update</param>
        /// <returns>True if Sale Updated Successfully, Otherwise False</returns>
        public bool UpdateCustomer(CustomerModel customer)
        {
            try
            {
                //create the SqlConnection object
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {

                    //retrieve the SQL Server instance version
                    string query = @"UPDATE dbo.Customer SET CustomerId = @CustomerId, FirstName = @FirstName, LastName = @LastName, Address = @Address, Phone = @Phone, StartDate = @StartDate WHERE CustomerId = @CustomerId;";
                    //create the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                    cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@Address", customer.Address);
                    cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                    cmd.Parameters.AddWithValue("@StartDate", customer.StartDate);

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
        /// Attempts to Delete a Customer from the Table Using the Customer ID
        /// </summary>
        /// <param name="customerID">The ID of the Customer to Delete</param>
        /// <returns>True if Customer Deleted Successfully, Otherwise False</returns>
        public bool DeleteCustomer(int customerID)
        {
            try
            {
                //create the SqlConnection object
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {

                    //retrieve the SQL Server instance version
                    string query = @"DELETE FROM dbo.Customer WHERE CustomerId = @customerId";

                    //create the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@customerId", customerID);

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
