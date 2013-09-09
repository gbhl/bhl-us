CREATE PROCEDURE dbo.IAItemStatusSelectAll

AS

BEGIN

SET NOCOUNT ON

SELECT	ItemStatusID,
		[Status],
		[Description]
FROM	dbo.IAItemStatus
ORDER BY
		ItemStatusID
		
END

