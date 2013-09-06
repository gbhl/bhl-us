
-- PaginationStatusDeleteAuto PROCEDURE
-- Generated 6/28/2007 2:15:43 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for PaginationStatus

CREATE PROCEDURE [dbo].[PaginationStatusDeleteAuto]

@PaginationStatusID INT

AS 

DELETE FROM [dbo].[PaginationStatus]

WHERE

	[PaginationStatusID] = @PaginationStatusID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PaginationStatusDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END


