
-- PaginationStatusSelectAuto PROCEDURE
-- Generated 6/28/2007 2:15:43 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for PaginationStatus

CREATE PROCEDURE [dbo].[PaginationStatusSelectAuto]

@PaginationStatusID INT

AS 

SET NOCOUNT ON

SELECT 

	[PaginationStatusID],
	[PaginationStatusName]

FROM [dbo].[PaginationStatus]

WHERE
	[PaginationStatusID] = @PaginationStatusID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PaginationStatusSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


