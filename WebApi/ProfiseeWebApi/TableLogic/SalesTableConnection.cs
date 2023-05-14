using Microsoft.Data.SqlClient;
using ProfiseeWebApi.Models;

namespace ProfiseeWebApi.TableLogic
{
    /// <summary>
    /// Handles Table Interactions for the Sales Table
    /// </summary>
    public class SalesTableConnection
    {
        //Connection Information to the Database
        private const string _ConnectionString = @"Server = localhost; Database = ProfiseeTables; Trusted_Connection = True; TrustServerCertificate=True";

        /// <summary>
        /// Handles Table Interactions for the Sales Table
        /// </summary>
        public SalesTableConnection()
        {

        }

        #region GET
        
        /// <summary>
        /// Retrieves the Commission Report for Sales People
        /// </summary>
        /// <returns>A List of Commission for All Sales People if Successful, Otherwise False</returns>
        public List<CommissionReportModel> GetCommissionReport()
        {
            List<CommissionReportModel> commissionReport = new List<CommissionReportModel>();

            List<ReadableSalesModel>? allSales = GetAllSales();
            if(allSales != null)
            {
                Dictionary<string, double> comissionFromPerson = new Dictionary<string, double>();
                foreach(var sale in allSales)
                {
                    string? key = sale.Salesperson;
                    if(comissionFromPerson.ContainsKey(key))
                    {
                        comissionFromPerson[key] += sale.Commission;
                    }
                    else
                    {
                        comissionFromPerson.Add(key, sale.Commission);
                    }
                }

                int count = 1;
                foreach(var salesPerson in comissionFromPerson)
                {
                    CommissionReportModel report = new CommissionReportModel()
                    {
                        ReportId = count,
                        Salesperson = salesPerson.Key,
                        Commission = Math.Round(salesPerson.Value, 2)
                    };
                    commissionReport.Add(report);
                    count++;
                }
            }

            return commissionReport;
        }



        /// <summary>
        /// Retrieves the List of Human Readable Sales
        /// </summary>
        /// <returns>A List of All Human Readable Sales if Successful, Otherwise Null</returns>
        public List<ReadableSalesModel>? GetAllSales()
        {
            List<ReadableSalesModel>? readableSales = new List<ReadableSalesModel>();
            List<SalesModel>? allSales = GetSalesFromTable();
            
            ProductTableConnection products = new ProductTableConnection();
            CustomerTableConnection customers = new CustomerTableConnection();
            SalespersonTableConnection salespeople = new SalespersonTableConnection();

            if(allSales != null)
            {
                foreach(var sale in allSales)
                {
                    CustomerModel? customer = customers.GetCustomer(sale.CustomerId);
                    SalespersonModel? salesperson = salespeople.GetSalesperson(sale.SalespersonId);
                    ProductsModel? product = products.GetProduct(sale.ProductId);

                    int saleID = sale.SalesId;
                    string? productName = product?.Name;
                    string customerName = $"{customer?.FirstName} {customer?.LastName}";
                    string salespersonName = $"{salesperson?.FirstName} {salesperson?.LastName}";
                    string? saleDate = sale.SalesDate;
                    double salePrice = product.SalePrice;
                    double saleCommission = Math.Round(product.SalePrice * product.CommissionPercentage, 2);


                    ReadableSalesModel readableSale = new ReadableSalesModel()
                    {
                        SaleId = saleID,
                        Product = productName,
                        Customer = customerName,
                        Salesperson = salespersonName,
                        Date = saleDate,
                        Price = salePrice,
                        Commission = saleCommission
                    };
                    readableSales.Add(readableSale);
                }
            }

            return readableSales;
        }

        /// <summary>
        /// Retrieves All Rows from the Sales Table
        /// </summary>
        /// <returns>A List of Entries in the Sales Table if Successful, Otherwise False</returns>
        private List<SalesModel>? GetSalesFromTable()
        {
            List<SalesModel> sales = new List<SalesModel>();

            try
            {
                //sql connection object
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {

                    //retrieve the SQL Server instance version
                    string query = @"SELECT * FROM dbo.Sales";


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
                            SalesModel sale = new SalesModel()
                            {
                                SalesId = dr.GetInt32(0),
                                ProductId = dr.GetInt32(1),
                                CustomerId = dr.GetInt32(2),
                                SalespersonId = dr.GetInt32(3),
                                SalesDate = dr.GetString(4)
                            };
                            sales.Add(sale);
                        }
                    }

                    //close data reader
                    dr.Close();

                    //close connection
                    conn.Close();

                    return sales;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }





        /// <summary>
        /// Attempts to Retrieve a Sale Based on the Sale ID
        /// </summary>
        /// <param name="saleID">The ID of the Sale to Retrieve</param>
        /// <returns>The Sale Information if Found, Otherwise False</returns>
        public SalesModel? GetSale(int saleID)
        {
            SalesModel? sale = null;
            try
            {
                //sql connection object
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {

                    //retrieve the SQL Server instance version
                    string query = $"SELECT * FROM dbo.Sales WHERE SalesId = {saleID}";


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
                            sale = new SalesModel()
                            {
                                SalesId = dr.GetInt32(0),
                                ProductId = dr.GetInt32(1),
                                CustomerId = dr.GetInt32(2),
                                SalespersonId = dr.GetInt32(3),
                                SalesDate = dr.GetString(4)
                            };
                        }
                    }

                    //close data reader
                    dr.Close();

                    //close connection
                    conn.Close();

                    return sale;

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
        /// Adds a Sale to the Table Using Human Readable Values
        /// </summary>
        /// <param name="sale">The Human Readable Sale Information</param>
        /// <returns>True if Information Added to Table Successfully, Otherwise False</returns>
        public bool AddHumanReadableSale(AddSaleModel sale)
        {
            ProductTableConnection productsTable = new ProductTableConnection();
            CustomerTableConnection customersTable = new CustomerTableConnection();
            SalespersonTableConnection salespeopleTable = new SalespersonTableConnection();

            List<ProductsModel>? products = productsTable.GetAllProducts();
            List<CustomerModel>? customers = customersTable.GetAllCustomers();
            List<SalespersonModel>? salespeople = salespeopleTable.GetAllSalespeople();
            List<SalesModel>? sales = GetSalesFromTable();

            int productID = 0;
            if(products != null)
            {
                foreach (var product in products)
                {
                    if(product.Name.Equals(sale.Product))
                    {
                        productID = product.ProductId;
                        break;
                    }
                }
            }

            int salespersonID = 0;
            if(salespeople != null)
            {
                string[] split = sale.Salesperson.Split(' ');
                foreach(var salesperson in salespeople)
                {
                    if (salesperson.FirstName.Equals(split[0]) && salesperson.LastName.Equals(split[1]))
                    {
                        salespersonID = salesperson.SalespersonID;
                        break;
                    }
                }
            }

            int customerID = 0;
            if (customers != null)
            {
                string[] split = sale.Customer.Split(' ');
                foreach (var customer in customers)
                {
                    if (customer.FirstName.Equals(split[0]) && customer.LastName.Equals(split[1]))
                    {
                        customerID = customer.CustomerId;
                        break;
                    }
                }
            }

            int saleID = 0;
            if(sales != null)
            {
                foreach(var tmpSale in sales)
                {
                    if(tmpSale.SalesId >= saleID)
                    {
                        saleID = tmpSale.SalesId;
                    }
                }
            }
            saleID++;


            SalesModel tableSale = new SalesModel()
            {
                ProductId = productID,
                SalesDate = DateTime.Today.ToString(),
                SalespersonId = salespersonID,
                CustomerId = customerID,
                SalesId = saleID
            };

            if(AddNewSale(tableSale))
            {
                return true;
            }
            else
            {
                return false;
            }
        
        }





        /// <summary>
        /// Attempts to Add a New Sale to the Sales Table
        /// </summary>
        /// <param name="sale">The Sale Information to Add to the Table</param>
        /// <returns>True if Sale Successfully Added, Otherwise False</returns>
        public bool AddNewSale(SalesModel sale)
        {
            try
            {
                //create the SqlConnection object
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {

                    //retrieve the SQL Server instance version
                    string query = @"INSERT INTO dbo.Sales (SalesId, ProductId, CustomerId, SalespersonId, SalesDate)";
                    query += " VALUES (@SalesId, @ProductId, @CustomerId, @SalespersonId, @SalesDate)";
                    //create the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SalesId", sale.SalesId);
                    cmd.Parameters.AddWithValue("@ProductId", sale.ProductId);
                    cmd.Parameters.AddWithValue("@CustomerId", sale.CustomerId);
                    cmd.Parameters.AddWithValue("@SalespersonId", sale.SalespersonId);
                    cmd.Parameters.AddWithValue("@SalesDate", sale.SalesDate);

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
        /// Attempts to Update a Sale Row in the Sales Table
        /// </summary>
        /// <param name="sale">The Sale Information to Update</param>
        /// <returns>True if Sale Updated Successfully, Otherwise False</returns>
        public bool UpdateSale(SalesModel sale)
        {
            try
            {
                //create the SqlConnection object
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {

                    //retrieve the SQL Server instance version
                    string query = @"UPDATE dbo.Sales SET ProductId = @productId, CustomerId = @customerId, SalespersonId = @salespersonId, SalesDate = @salesDate WHERE SalesId = @salesId;";
                    //create the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@salesId", sale.SalesId);
                    cmd.Parameters.AddWithValue("@productId", sale.ProductId);
                    cmd.Parameters.AddWithValue("@customerId", sale.CustomerId);
                    cmd.Parameters.AddWithValue("@salespersonId", sale.SalespersonId);
                    cmd.Parameters.AddWithValue("@salesDate", sale.SalesDate);

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
        /// Attempts to Delete a Sale from the Table Using the Sale ID
        /// </summary>
        /// <param name="saleID">The ID of the Sale to Delete</param>
        /// <returns>True if Sale Deleted Successfully, Otherwise False</returns>
        public bool DeleteSale(int saleID)
        {
            try
            {
                //create the SqlConnection object
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {

                    //retrieve the SQL Server instance version
                    string query = @"DELETE FROM dbo.Sales WHERE SalesId = @salesId";

                    //create the SqlCommand object
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@salesId", saleID);

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
