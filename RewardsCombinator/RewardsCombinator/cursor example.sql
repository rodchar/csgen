USE Northwind
/* First, declare items in the cursor */
DECLARE @cur_CustomerID nchar (5),
@cur_ContactTitle nvarchar(30)
/* Next, declare the cursor itself */
DECLARE changes_cursor cursor
for SELECT customerid, contacttitle
FROM dbo.customers
OPEN changes_cursor
/* first fetch starts the process */
FETCH changes_cursor INTO @cur_CustomerID, @cur_ContactTitle
WHILE @@fetch_status = 0 /* while there's still records to process */
BEGIN
IF @cur_ContactTitle = 'owner'
BEGIN
UPDATE orders SET [freight] = 0
WHERE CustomerID = @cur_CustomerID
END
/* keep fetching till done */
FETCH changes_cursor INTO @cur_CustomerID, @cur_ContactTitle
END
CLOSE changes_cursor
DEALLOCATE changes_cursor

--Read more: How to Create a Cursor in SQL Server | eHow.com http://www.ehow.com/how_5059139_create-cursor-sql-server.html#ixzz28xCCtfWq