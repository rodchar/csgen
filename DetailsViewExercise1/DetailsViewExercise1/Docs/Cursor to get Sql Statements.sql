/*
http://www.codeproject.com/Tips/277847/How-to-use-Cursor-in-Sql
*/

DECLARE @tableName NVARCHAR(50) = 'Customers'
DECLARE @stmt NVARCHAR(MAX);

SELECT * FROM SpecialTable1 WHERE TableName = @tableName;

DECLARE @SqlStatementCursor CURSOR
SET @SqlStatementCursor = CURSOR FAST_FORWARD
FOR
SELECT SqlStatements
FROM SpecialTable1 WHERE TableName = @tableName;
OPEN @SqlStatementCursor
	FETCH NEXT FROM @SqlStatementCursor
	INTO @stmt
	WHILE @@FETCH_STATUS = 0
	BEGIN

		EXEC (@stmt)

		FETCH NEXT FROM @SqlStatementCursor
		INTO @stmt
	END
CLOSE @SqlStatementCursor
DEALLOCATE @SqlStatementCursor