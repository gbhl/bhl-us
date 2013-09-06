
-- PaginationStatusUpdateAuto PROCEDURE
-- Generated 6/28/2007 2:15:43 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for PaginationStatus

CREATE PROCEDURE [dbo].[PaginationStatusUpdateAuto]

@PaginationStatusID INT,
@PaginationStatusName NVARCHAR(50)

AS 

SET NOCOUNT ON

UPDATE [dbo].[PaginationStatus]

SET

	[PaginationStatusID] = @PaginationStatusID,
	[PaginationStatusName] = @PaginationStatusName

WHERE
	[PaginationStatusID] = @PaginationStatusID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PaginationStatusUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[PaginationStatusID],
		[PaginationStatusName]

	FROM [dbo].[PaginationStatus]
	
	WHERE
		[PaginationStatusID] = @PaginationStatusID
	
	RETURN -- update successful
END


