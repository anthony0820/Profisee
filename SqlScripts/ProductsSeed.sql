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