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