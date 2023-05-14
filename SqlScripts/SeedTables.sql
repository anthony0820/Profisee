-- Insert rows into table 'Sales'
INSERT INTO dbo.Sales
   ([SalesId],[ProductId],[CustomerId],[SalespersonId],[SalesDate])
VALUES
   ( 1, 1, 1, 1, '08/20/1999'),
   ( 2, 1, 2, 1, '08/20/1999'),
   ( 3, 2, 3, 2, '08/20/1999'),
   ( 4, 3, 3, 3, '08/20/1999'),
   ( 5, 1, 2, 4, '08/20/1999')
GO

-- Insert rows into table 'Salesperson'
INSERT INTO dbo.Salesperson
   ([SalespersonId],[FirstName],[LastName],[Address],[Phone],[StartDate],[TerminationDate],[Manager])
VALUES
   ( 1, 'Anne', 'King', '3534 Holly Hwy', '(678)-234-3633', '08/20/1999', '09/20/1999', 2),
   ( 2, 'Tom', 'Copenhaver', '1134 Hester Lane', '(792)-242-3423', '01/02/2023', '08/20/2024', 2),
   ( 3, 'Nathan', 'Brackett', '2462 Cobber Lane', '(262)-923-2319', '03/15/2023', '09/23/2024', 2),
   ( 4, 'Cody', 'Banks', '1041 Mary Road', '(823)-223-9134', '04/14/2023', '10/12/2023', 2),
   ( 5, 'Clay', 'Costley', '1513 Juniper Court', '(252)-262-8124', '10/20/2022', '08/20/2025', 2)
GO

-- Insert rows into table 'Products'
INSERT INTO dbo.Products
   ([ProductId],[Name],[Manufacturer],[Style],[PurchasePrice],[SalePrice],[QtyOnHand],[CommissionPercentage])
VALUES
   ( 1, 'Super Bike', 'Yamaha', 'Chopper', 172.99, 200.99, 100, 0.05),
   ( 2, 'Low Rider', 'Discus', 'Supreme', 209.99, 249.99, 50, 0.10),
   ( 3, 'Cruiser', 'ATL Bikes', 'Classic', 105.99, 172.99, 75, 0.05),
   ( 4, 'Hero Bike', 'Amazon', 'Starter', 97.99, 129.99, 80, 0.10),
   ( 5, 'Night Rider', 'Ridge Inc.', 'Racer', 149.99, 205.99, 125, 0.10)
GO

-- Insert rows into table 'Discount'
INSERT INTO dbo.Discount
   ([DiscountId],[ProductId],[BeginDate],[EndDate],[DiscountPercentage])
VALUES
   ( 1, 1, '04/20/2023', '09/20/2023', 0.05),
   ( 2, 2, '01/15/2023', '10/15/2023', 0.10),
   ( 3, 3, '02/14/2023', '08/14/2023', 0.20),
   ( 4, 4, '01/25/2023', '07/25/2023', 0.07),
   ( 5, 5, '03/13/2023', '11/13/2023', 0.05)
GO

-- Insert rows into table 'Customers'
INSERT INTO dbo.Customer
   ([CustomerId],[FirstName],[LastName],[Address],[Phone],[StartDate])
VALUES
   ( 1, 'Michael', 'Huth', '9113 McClure Drive', '(513)-623-2235', '03/05/2023'),
   ( 2, 'Jay', 'Lovaas', '6232 Romeo Circle', '(264)-124-9151', '05/15/2023'),
   ( 3, 'Jack', 'Houston', '6264 Cloverfield Lane', '(363)-252-9576', '04/05/2023'),
   ( 4, 'Spencer', 'Houston', '6264 Cloverfield Lane', '(363)-236-7353', '04/05/2023'),
   ( 5, 'Will', 'Tuten', '9072 Canton Highway', '(121)-636-2363', '01/03/2023')
GO