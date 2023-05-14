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