DECLARE	@page_size INT = 3, @page_nbr INT = 0;
IF object_id('tempdb..#Payload') IS NOT NULL DROP TABLE #Payload;
IF (@page_nbr = 0) SELECT @page_nbr = 1;

SELECT 0 as 'TR', 0 as 'TP',  
-- Todo: Figure out sort default
-- http://stackoverflow.com/a/13595691/139698
ROW_NUMBER() OVER ( ORDER BY Name ) AS 'Seq'	
, *
INTO #Payload
FROM Products

DECLARE @total_pages INT, @total_recs INT;
DECLARE @total_pages2 DECIMAL(18,2);
SELECT @total_recs = COUNT(*) FROM #Payload;
SELECT @total_pages = @total_recs / @page_size;
SELECT @total_pages2 = @total_recs / (@page_size * 1.00)

IF (@total_pages < 1) select @total_pages = 1;
IF (@total_pages < @total_pages2) select @total_pages = @total_pages + 1;

UPDATE #Payload
SET TR = @total_recs,
TP = @total_pages;

SELECT *
FROM #Payload
WHERE seq > (@page_nbr - 1) * @page_size
  AND seq <= @page_nbr * @page_size;