CREATE PROCEDURE dbo.ItemSelectNextStartDate

AS

BEGIN

-- Selects the Date from which to start processing the next harvest.
-- Use the date prior to the most recent CreationDate.

SET NOCOUNT ON

SELECT TOP 1
		DATEFROMPARTS(
	        DATEPART(YEAR, CreationDate), 
			DATEPART(MONTH, CreationDate), 
			DATEPART(DAY, CreationDate) - 1
			) AS StartDate
FROM    dbo.Item
ORDER BY
        ItemID DESC

END

GO
