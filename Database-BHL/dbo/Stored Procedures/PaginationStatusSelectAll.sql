CREATE PROCEDURE [dbo].[PaginationStatusSelectAll]

AS 

SET NOCOUNT ON

SELECT 

	[PaginationStatusID],
	[PaginationStatusName]

FROM [dbo].[PaginationStatus]


IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PaginationStatusSelectAll. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


