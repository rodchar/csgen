DECLARE @stmt NVARCHAR(MAX);
DECLARE @tableName NVARCHAR(50) = 'Customers'

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