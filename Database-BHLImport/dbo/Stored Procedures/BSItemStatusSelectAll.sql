CREATE PROCEDURE dbo.BSItemStatusSelectAll

AS

BEGIN

SET NOCOUNT ON

SELECT	ItemStatusID,
		[Status],
		[Description]
FROM	dbo.BSItemStatus
ORDER BY
		ItemStatusID

END


