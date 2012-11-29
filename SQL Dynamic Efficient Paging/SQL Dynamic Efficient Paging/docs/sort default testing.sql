-- http://stackoverflow.com/a/13595691/139698

DECLARE @table_name nvarchar(100) = 'vwReceiptItemNamesProducts'
DECLARE @sort_column nvarchar(100) 
DECLARE @search_column nvarchar(100) 

/* Makes column 1 the sort default */
/* Makes column 2 the search default */
/* begin region */
SELECT @sort_column = columns.name FROM sys.columns inner join sys.tables ON tables.object_id = columns.object_id
WHERE tables.name = @table_name and column_id = 1
SELECT @search_column = columns.name FROM sys.columns inner join sys.tables ON tables.object_id = columns.object_id
WHERE tables.name = @table_name and column_id = 2

IF @sort_column IS NULL
BEGIN
	SELECT @sort_column = columns.name FROM sys.columns inner join sys.views ON views.object_id = columns.object_id
	WHERE views.name = @table_name and column_id = 1
	SELECT @search_column = columns.name FROM sys.columns inner join sys.views ON views.object_id = columns.object_id
	WHERE views.name = @table_name and column_id = 2
END
/* end region */

EXEC ('
select *, 
ROW_NUMBER() OVER ( ORDER BY ' + @sort_column + ' ) AS ''Seq''   
from ' + @table_name);

select @search_column;
select * from vwReceiptItemNamesProducts where 2 like 'ba%'
/*
DECLARE @table_name nvarchar(100) = 'vwReceiptItemNamesProducts'
DECLARE @sort_column nvarchar(100) 
SELECT @sort_column = columns.name FROM sys.columns inner join sys.tables ON tables.object_id = columns.object_id
WHERE tables.name = @table_name and column_id = 1
SELECT @sort_column
*/