-- get data

declare @CustomerTable table (
    CustomerID int identity(1,1),
    FirstName nvarchar(50),
    LastName nvarchar(50)
)

insert into @CustomerTable (FirstName, LastName)
values
    ('John', 'Doe'),
    ('Jane', 'Smith'),
    ('Michael', 'Johnson'),
    ('Emily', 'Williams'),
    ('William', 'Brown'),
    ('Emma', 'Jones'),
    ('Matthew', 'Garcia'),
    ('Olivia', 'Miller'),
    ('Daniel', 'Davis'),
    ('Ava', 'Martinez'),
    ('Alexander', 'Martinez'),
    ('Sophia', 'Lopez'),
    ('James', 'Gonzalez'),
    ('Isabella', 'Wilson'),
    ('Benjamin', 'Anderson'),
    ('Mia', 'Thomas'),
    ('Jacob', 'Taylor'),
    ('Charlotte', 'Moore'),
    ('William', 'Jackson'),
    ('Amelia', 'White'),
    ('Ethan', 'Harris'),
    ('Harper', 'Martin'),
    ('Michael', 'Thompson'),
    ('Evelyn', 'Garcia'),
    ('Alexander', 'Robinson'),
    ('Abigail', 'Lewis'),
    ('Daniel', 'Lee'),
    ('Sophia', 'Walker'),
    ('Matthew', 'Hall'),
    ('Emily', 'Young'),
    ('Matthew', 'King'),
    ('Elizabeth', 'Scott'),
    ('David', 'Green'),
    ('Victoria', 'Baker'),
    ('Jacob', 'Hill'),
    ('Ava', 'Adams'),
    ('James', 'Nelson'),
    ('Emma', 'Morris'),
    ('Alexander', 'Wright'),
    ('Ella', 'Sanchez'),
    ('William', 'Ross'),
    ('Chloe', 'Mitchell'),
    ('Daniel', 'Perez'),
    ('Amelia', 'Howard'),
    ('Mason', 'Kim'),
    ('Grace', 'Collins'),
    ('Lucas', 'Nguyen'),
    ('Avery', 'Cook')

declare @CustomerCount int = 1
while @CustomerCount <= 50
begin
    declare @FirstName nvarchar(50)
    declare @LastName nvarchar(50)

    select @FirstName = FirstName, @LastName = LastName
    from @CustomerTable
    where CustomerID = @CustomerCount

    execute Customers.CreateCustomer @FirstName, @LastName

    set @CustomerCount = @CustomerCount + 1
end

declare @AccountsReceivableTable table (
    CustomerID int,
    InvoiceID int identity(1,1),
    AmountOwed decimal(10,2),
    DueDate date
)

insert into @AccountsReceivableTable (CustomerID, AmountOwed, DueDate)
values
    (1, 2277.00, '2024-07-01'),
    (2, 3953.00, '2024-07-03'),
    (3, 4726.00, '2024-07-05'),
    (4, 1414.00, '2024-07-07'),
    (5, 627.00, '2024-07-09'),
    (6, 1784.00, '2024-07-11'),
    (7, 1634.00, '2024-07-13'),
    (8, 3958.00, '2024-07-15'),
    (9, 2156.00, '2024-07-17'),
    (10, 1347.00, '2024-07-19'),
    (11, 2166.00, '2024-07-21'),
    (12, 820.00, '2024-07-23'),
    (13, 2325.00, '2024-07-25'),
    (14, 3613.00, '2024-07-27'),
    (15, 2389.00, '2024-07-29'),
    (16, 4130.00, '2024-07-31'),
    (17, 2007.00, '2024-08-02'),
    (18, 3027.00, '2024-08-04'),
    (19, 2591.00, '2024-08-06'),
    (20, 3940.00, '2024-08-08'),
    (21, 3888.00, '2024-08-10'),
    (22, 2975.00, '2024-08-12'),
    (23, 4470.00, '2024-08-14'),
    (24, 2291.00, '2024-08-16'),
    (25, 3393.00, '2024-08-18'),
    (26, 3588.00, '2024-08-20'),
    (27, 3286.00, '2024-08-22'),
    (28, 2293.00, '2024-08-24'),
    (29, 4353.00, '2024-08-26'),
    (30, 3315.00, '2024-08-28'),
    (31, 4900.00, '2024-08-30'),
    (32, 794.00, '2024-09-01'),
    (33, 4424.00, '2024-09-03'),
    (34, 4505.00, '2024-09-05'),
    (35, 2643.00, '2024-09-07'),
    (36, 2217.00, '2024-09-09'),
    (37, 4193.00, '2024-09-11'),
    (38, 2893.00, '2024-09-13'),
    (39, 4120.00, '2024-09-15'),
    (40, 3352.00, '2024-09-17'),
    (41, 2355.00, '2024-09-19'),
    (42, 3219.00, '2024-09-21'),
    (43, 3064.00, '2024-09-23'),
    (44, 4893.00, '2024-09-25'),
    (45, 272.00, '2024-09-27'),
    (46, 1299.00, '2024-09-29'),
    (47, 4725.00, '2024-10-01'),
    (48, 1900.00, '2024-10-03'),
    (49, 4927.00, '2024-10-05'),
    (50, 4011.00, '2024-10-07')

declare @InvoiceCount int = 1

while @InvoiceCount <= 50
begin
    declare @CustomerID int
    declare @AmountOwed decimal(10,2)
    declare @DueDate date

    select @CustomerID = CustomerID, @AmountOwed = AmountOwed, @DueDate = DueDate
    from @AccountsReceivableTable
    where InvoiceID = @InvoiceCount

    execute Customers.CreateAccountsReceivable @CustomerID, @InvoiceCount, @AmountOwed, @DueDate

    set @InvoiceCount = @InvoiceCount + 1
end

execute HumanResources.CreateDebtCollector 'John', 'Smith';
execute HumanResources.CreateDebtCollector 'Jane', 'Johnson';
execute HumanResources.CreateDebtCollector 'Michael', 'Williams';
execute HumanResources.CreateDebtCollector 'Emily', 'Brown';
execute HumanResources.CreateDebtCollector 'William', 'Jones';
execute HumanResources.CreateDebtCollector 'Emma', 'Garcia';
execute HumanResources.CreateDebtCollector 'Matthew', 'Miller';
execute HumanResources.CreateDebtCollector 'Olivia', 'Davis';
execute HumanResources.CreateDebtCollector 'Daniel', 'Martinez';
execute HumanResources.CreateDebtCollector 'Ava', 'Lopez';