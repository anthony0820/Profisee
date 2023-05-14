using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using ProfiseeWebApi.Models;
using System.Net;
using System.Numerics;

namespace ProfiseeWebApi.TableLogic
{
    /// <summary>
    /// Handles Table Interactions for the Salesperson Table
    /// </summary>
    public class SalespersonTableConnection
    {
        //Connection Information to the Database
        private const string _ConnectionString = @"Server = localhost; Database = ProfiseeTables; Trusted_Connection = True; TrustServerCertificate=True";

        /// <summary>
        /// Handles Table Interactions for the Salesperson Table
        /// </summary>
        public SalespersonTableConnection()
        {

        }

        #region GET

        /// <summary>
        /// Retrieves All Salespeople in the Salesperson Table
        /// </summary>
        /// <returns>A List of All Salespeople if Successful, Otherwise Null</returns>
        public List<SalespersonModel>? GetAllSalespeople()
        {
            List<SalespersonModel> salesPeople = new List<SalespersonModel>();

            try
            {
                //sql connection object
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {

                    //retrieve the SQL Server instance version
                    string query = @"SELECT * FROM dbo.Salesperson";


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
                            SalespersonModel salesPerson = new SalespersonModel()
                            {
                                SalespersonID = dr.GetInt32(0),
                                FirstName = dr.GetString(1),
                                LastName = dr.GetString(2),
                                Address = dr.GetString(3),
                                Phone = dr.GetString(4),
                                StartDate = dr.GetString(5),
                                TerminationDate = dr.GetString(6),
                                Manager = dr.GetInt32(7)
                            };
                            salesPeople.Add(salesPerson);
                        }
                    }

                    //close data reader
                    dr.Close();

                    //close connection
                    conn.Close();

                    return salesPeople;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Attempts to Retrieve a Salesperson Based on the Salesperson ID
        /// </summary>
        /// <param name="salespersonId">The ID of the Salesperson to Retrieve</param>
        /// <returns>The Salesperson Information if Found, Otherwise False</returns>
        public SalespersonModel? GetSalesperson(int salespersonId)
        {
            SalespersonModel? salesperson = null;
            try
            {
                //sql connection object
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {

                    //retrieve the SQL Server instance version
                    string query = $"SELECT * FROM dbo.Salesperson WHERE SalespersonId = {salespersonId}";


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
                            salesperson = new SalespersonModel()
                            {
                                SalespersonID = dr.GetInt32(0),
                                FirstName = dr.GetString(1),
                                LastName = dr.GetString(2),
                                Address = dr.GetString(3),
                                Phone = dr.GetString(4),
                                StartDate = dr.GetString(5),
                                TerminationDate = dr.GetString(6),
                                Manager = dr.GetInt32(7)
                            };
                        }
                    }

                    //close data reader
                    dr.Close();

                    //close connection
                    conn.Close();

                    return salesperson;

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
        /// Attempts to Add a New Salesperson to the Salesperson Table
        /// </summary>
        /// <param name="salesperson">The Salesperson Information to Add to the Table</param>
        /// <returns>True if Salesperson Successfully Added, Otherwise False</returns>
        public bool AddNewSalesperson(SalespersonModel salesperson)
        {
            try
            {
                //create the SqlConnection object
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {

                    //retrieve the SQL Server instance version
                    string query = @"INSERT INTO dbo.Salesperson (SalespersonId, FirstName, LastName, Address, Phone, StartDate, TerminationDate, Manager)";
                    query += " VALUES (@SalespersonId, @FirstName, @LastName, @Address, @Phone, @StartDate, @TerminationDate, @Manager)";
                    //create the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SalespersonId", salesperson.SalespersonID);
                    cmd.Parameters.AddWithValue("@FirstName", salesperson.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", salesperson.LastName);
                    cmd.Parameters.AddWithValue("@Address", salesperson.Address);
                    cmd.Parameters.AddWithValue("@Phone", salesperson.Phone);
                    cmd.Parameters.AddWithValue("@StartDate", salesperson.StartDate);
                    cmd.Parameters.AddWithValue("@TerminationDate", salesperson.TerminationDate);
                    cmd.Parameters.AddWithValue("@Manager", salesperson.Manager);

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
        /// Attempts to Update a Salesperson Row in the Salesperson Table
        /// </summary>
        /// <param name="salesperson">The Salesperson Information to Update</param>
        /// <returns>True if Salesperson Updated Successfully, Otherwise False</returns>
        public bool UpdateSalesperson(SalespersonModel salesperson)
        {
            try
            {
                //create the SqlConnection object
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {

                    //retrieve the SQL Server instance version
                    string query = @"UPDATE dbo.Salesperson SET FirstName = @FirstName, LastName = @LastName, Address = @Address, Phone = @Phone, StartDate = @StartDate, TerminationDate = @TerminationDate, Manager = @Manager WHERE SalespersonId = @SalespersonId;";
                    //create the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SalespersonId", salesperson.SalespersonID);
                    cmd.Parameters.AddWithValue("@FirstName", salesperson.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", salesperson.LastName);
                    cmd.Parameters.AddWithValue("@Address", salesperson.Address);
                    cmd.Parameters.AddWithValue("@Phone", salesperson.Phone);
                    cmd.Parameters.AddWithValue("@StartDate", salesperson.StartDate);
                    cmd.Parameters.AddWithValue("@TerminationDate", salesperson.TerminationDate);
                    cmd.Parameters.AddWithValue("@Manager", salesperson.Manager);

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
        /// Attempts to Delete a Salesperson from the Table Using the Salesperson ID
        /// </summary>
        /// <param name="salespersonID">The ID of the Salesperson to Delete</param>
        /// <returns>True if Salesperson Deleted Successfully, Otherwise False</returns>
        public bool DeleteSalesperson(int salespersonID)
        {
            try
            {
                //create the SqlConnection object
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {

                    //retrieve the SQL Server instance version
                    string query = @"DELETE FROM dbo.Salesperson WHERE SalespersonId = @salespersonId";

                    //create the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@salespersonId", salespersonID);

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
