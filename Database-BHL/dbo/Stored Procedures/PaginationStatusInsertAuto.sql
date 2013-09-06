
-- PaginationStatusInsertAuto PROCEDURE
-- Generated 6/28/2007 2:15:43 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for PaginationStatus

CREATE PROCEDURE [dbo].[PaginationStatusInsertAuto]

@PaginationStatusID INT,
@PaginationStatusName NVARCHAR(50)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[PaginationStatus]
(
	[PaginationStatusID],
	[PaginationStatusName]
)
VALUES
(
	@PaginationStatusID,
	@PaginationStatusName
)

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PaginationStatusInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[PaginationStatusID],
		[PaginationStatusName]	

	FROM [dbo].[PaginationStatus]
	
	WHERE
		[PaginationStatusID] = @PaginationStatusID
	
	RETURN -- insert successful
END


