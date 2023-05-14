-- Insert rows into table 'Salesperson'
INSERT INTO dbo.Salesperson
   ([SalespersonId],[FirstName],[LastName],[Address],[Phone],[StartDate],[TerminationDate],[Manager])
VALUES
   ( 1, 'Anthony', 'Puhalovich', 'Addy', '(678)-234-3633', '08/20/1999', '09/20/1999', 2),
   ( 2, 'Tom', 'Copenhaver', '1134 Hester Lane', '(792)-242-3423', '01/02/2023', '08/20/2024', 2),
   ( 3, 'Nathan', 'Brackett', '2462 Cobber Lane', '(262)-923-2319', '03/15/2023', '09/23/2024', 2),
   ( 4, 'Cody', 'Banks', '1041 Mary Road', '(823)-223-9134', '04/14/2023', '10/12/2023', 2),
   ( 5, 'Clay', 'Costley', '1513 Juniper Court', '(252)-262-8124', '10/20/2022', '08/20/2025', 2)
GO