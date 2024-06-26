--database usage
create database ChargesDB

use ChargesDB
Go

CREATE LOGIN userLogin WITH PASSWORD = 'easy1234';
GO

CREATE USER userDev FOR LOGIN userLogin;
GO

Grant execute to userDev

Create Schema Financial
Create Schema HumanResources
Create Schema Customers

--Tables
create table HumanResources.DebtCollectors
(
	debtCollectorID int identity (1,1) primary key not null,
	firstName nvarchar(50) not null,
	lastName nvarchar(50) not null,
)

create table Customers.Customers
(
	customerID int identity (1,1) primary key not null,
	firstName nvarchar(50) not null,
	lastName nvarchar(50) not null
)

create table Customers.AccountsReceivable
(
   accountID int identity(1,1) primary key not null,
   customerID int foreign key references Customers.Customers (customerID),
   invoiceID int,
   amountOwed decimal(10,2),
   dueDate date
)

create table Financial.CollectionRoutes
(
  accountID int not null foreign key references Customers.Customers (customerID),
  debtCollectorID int not null foreign key references HumanResources.DebtCollectors (debtCollectorID),
  assignmentDate datetime not null,
  PaymentStatus bit not null,
  primary key (accountID,debtCollectorID)
)

-- Other data may be necessary, but we are using these for testing purposes. (Tables)

-- Stored procedures DebtCollector
create procedure HumanResources.sp_CreateDebtCollector
	@firstName nvarchar(50),
	@lastName nvarchar(50)
as
begin
	set noCount on;

	if(LEN(@firstName) = 0)
	begin
			RAISERROR('The first name cant be empty.', 16, 1)
			return;
	end

	if(LEN(@lastName) = 0)
	begin
			RAISERROR('The last name cant be empty.', 16, 1)
			return;
	end
	begin try
		begin transaction
			insert into HumanResources.DebtCollectors values (@firstName,@lastName)
		commit transaction
	end try
	begin catch
	   if(@@TRANCOUNT > 0)
	   Rollback transaction;
	   throw;
	end catch
end

create procedure HumanResources.sp_UpdateDebtCollector 
    @DebtCollectorID int,
	@firstName nvarchar(50),
	@lastName nvarchar(50)
as
begin 
	set noCount on;

	if(LEN(@firstName) = 0)
	begin
			RAISERROR('The first name cant be empty.', 16, 1)
			return;
	end

	if(LEN(@lastName) = 0)
	begin
			RAISERROR('The last name cant be empty.', 16, 1)
			return;
	end

	if((select count(*) from HumanResources.DebtCollectors where debtCollectorID = @DebtCollectorID ) = 0)
	begin
		RAISERROR('The record does not exist.', 16, 1)
	    return;
	end
	
	begin try
		begin transaction
			update HumanResources.DebtCollectors 
			set firstName = @firstName, lastName = @lastName
			where debtCollectorID = @DebtCollectorID
		commit transaction
	end try
	begin catch
	   if(@@TRANCOUNT > 0)
	   Rollback transaction;
	   throw;
	end catch
end

create procedure HumanResources.sp_GetAllDebtCollectorPage
    @Offset int,
    @PageSize int
as
begin
    set nocount on;

    declare @StartRow int, @EndRow int;
    set @StartRow = (@Offset - 1) * @PageSize + 1;
    set @EndRow = @Offset * @PageSize;

    select debtCollectorID, FirstName, LastName
    from (
        select debtCollectorID, FirstName, LastName,
               ROW_NUMBER() over (order by debtCollectorID) as RowNum
        from HumanResources.DebtCollectors
    ) as DebtCollectorsRowNum
    where RowNum BETWEEN @StartRow AND @EndRow;
END;

create procedure HumanResources.sp_GetOneByOneDebtCollector
    @DebtCollectorID int
as
begin
		set nocount on;
		if((select count(*) from HumanResources.DebtCollectors where debtCollectorID = @DebtCollectorID ) = 0)
		begin
			RAISERROR('The record does not exist.', 16, 1)
			return;
		end

        select * 
        from HumanResources.DebtCollectors
		where debtCollectorID = @DebtCollectorID
end;

-- Stored procedures Customers

create procedure Customers.sp_CreateCustomer
	@firstName nvarchar(50),
	@lastName nvarchar(50)
as
begin
	set noCount on;

	if(LEN(@firstName) = 0)
	begin
		RAISERROR('The first name cant be empty.', 16, 1)
		return;
	end

	if(LEN(@lastName) = 0)
	begin
		RAISERROR('The last name cant be empty.', 16, 1)
		return;
	end
	
	begin try
		begin transaction
			insert into Customers.Customers values (@firstName,@lastName)
		commit transaction
	end try
	begin catch
	   if(@@TRANCOUNT > 0)
	   Rollback transaction;
	   throw;
	end catch
end

create procedure Customers.sp_UpdateCustomer 
    @CustomerID int,
	@firstName nvarchar(50),
	@lastName nvarchar(50)
as
begin 
	set noCount on;

	if(LEN(@firstName) = 0)
	begin
		RAISERROR('The first name cant be empty.', 16, 1)
		return;
	end

	if(LEN(@lastName) = 0)
	begin
		RAISERROR('The last name cant be empty.', 16, 1)
		return;
	end

	if((select count(*) from Customers.Customers where customerID = @CustomerID ) = 0)
	begin
		RAISERROR('The record  does not exist.', 16, 1)
		return;
	end
	
	begin try
		begin transaction
			update Customers.Customers 
			set firstName = @firstName, lastName = @lastName
			where customerID = @CustomerID
		commit transaction
	end try
	begin catch
	   if(@@TRANCOUNT > 0)
	   Rollback transaction;
	   throw;
	end catch
end

create procedure Customers.sp_GetAllCustomersPage
    @Offset int,
    @PageSize int
as
begin
    set nocount on;

    declare @StartRow int, @EndRow int;
    set @StartRow = (@Offset - 1) * @PageSize + 1;
    set @EndRow = @Offset * @PageSize;

    select customerID, FirstName, LastName
    from (
        select customerID, FirstName, LastName,
               ROW_NUMBER() over (order by customerID) as RowNum
        from Customers.Customers
    ) as CustomerRowNum
    where RowNum BETWEEN @StartRow AND @EndRow;
end

create procedure Customers.sp_GetOneByOneCustomer
    @CustomerID int
as
begin
		set nocount on;
		if((select count(*) from Customers.Customers where customerID = @CustomerID ) = 0)
		begin
			RAISERROR('The record does not exist.', 16, 1)
			return;
		end

        select * 
        from Customers.Customers
		where customerID = @CustomerID
end;

-- Stored procedures AccountsReceivable
alter procedure Customers.sp_CreateAccountsReceivable
@CustomerID int, @InvoiceID int, @AmountOwed decimal(10,2),
@DueDate date
as
begin
   set nocount on;
		if((select count(*) from Customers.Customers where customerID = @CustomerID ) = 0)
		begin
			RAISERROR('The customer does not exist', 16, 1)
			return;
		end

		if((select count(*) from Customers.AccountsReceivable where invoiceID = @InvoiceID ) != 0)
		begin
			RAISERROR('The account receivable already exist.', 16, 1)
			return;
		end

		if((select count(*) from Customers.AccountsReceivable where customerID = @CustomerID ) != 0)
		begin
			RAISERROR('The client already has an account.', 16, 1)
			return;
		end

		if(@DueDate <= GETDATE())
		begin
			RAISERROR('The expiration date cannot be less than or equal to todays date.', 16, 1)
			return;
		end
		
		if(@AmountOwed <= 0)
		begin
			RAISERROR('The account cannot be less than zero.', 16, 1)
			return;
		end

		begin try
			begin transaction
			    insert into Customers.AccountsReceivable values (@CustomerID,@InvoiceID,@AmountOwed,@DueDate)
			commit transaction
		end try
		begin catch
		   if(@@TRANCOUNT > 0)
				Rollback transaction;
		   throw;
		end catch
end

-- Stored procedures CollectionRoutes This is the stored procedure that will be executed after sorting the amounts

create procedure Financial.GetSumByCollector
as
begin
    select
        debtCollectors.debtCollectorID as [Collector ID],
        debtCollectors.firstName + ' ' + debtCollectors.lastName as [Collector Name],
        COUNT(accountsReceivable.accountID) as [NumberRoutes],
        SUM(accountsReceivable.amountOwed) as [TotalAmount]
    from 
        Financial.CollectionRoutes collectionRutes
    inner join 
        Customers.AccountsReceivable accountsReceivable
        on collectionRutes.accountID = accountsReceivable.accountID
    inner join
        HumanResources.DebtCollectors debtCollectors
        on collectionRutes.debtCollectorID = debtCollectors.debtCollectorID
    group by
        debtCollectors.debtCollectorID,
        debtCollectors.firstName,
        debtCollectors.lastName
    order by
        debtCollectors.firstName;
end;



create procedure financial.sp_AssignBalancesToCollectors
as
begin
    set nocount on;
    delete from [financial].[collectionroutes];
    -- variable to count the total number of balances to be assigned
    declare @totalbalances int;
    select @totalbalances = count(accountid) from customers.accountsreceivable;

    -- variable to count the number of managers
    declare @totalcollectors int;
    select @totalcollectors = count(debtcollectorid) from humanresources.debtcollectors;

    -- check if there are managers and balance sheets to avoid division by zero
    if @totalcollectors > 0 and @totalbalances > 0
    begin
        -- calculate the number of iterations needed (round up)
        declare @iterations int;
        set @iterations = ceiling(convert(float, @totalbalances) / @totalcollectors);

        -- declare cursor for balances sorted in descending orde
        declare amounts_cursor cursor for
        select [amountowed], accountid
        from [customers].[accountsreceivable]
        order by [amountowed] desc;

        declare @debtcollectorid int;
        declare @amountowed decimal(18, 2);
        declare @accountid int;

        open amounts_cursor;

        declare @collectorcounter int;
        set @collectorcounter = 1;

        fetch next from amounts_cursor into @amountowed, @accountid; -- initial fetch outside while loop

        while @@fetch_status = 0
        begin

            select @debtcollectorid = debtcollectorid
            from (
                select debtcollectorid, row_number() over (order by debtcollectorid) as rownum
                from humanresources.debtcollectors
            ) as rowranked
            where rownum = @collectorcounter;

            insert into [financial].[collectionroutes] 
            values (@accountid, @debtcollectorid, getdate(), 0);

            set @collectorcounter = @collectorcounter + 1;

            if @collectorcounter > @totalcollectors
            begin
                set @collectorcounter = 1;
            end

            fetch next from amounts_cursor into @amountowed, @accountid;
        end

        close amounts_cursor;
        deallocate amounts_cursor;

        exec financial.getsumbycollector;
    end
    else
    begin
        raiserror('there are no managers or balances to assign.', 16, 1);
    end
end;

