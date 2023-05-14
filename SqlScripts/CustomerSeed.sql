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