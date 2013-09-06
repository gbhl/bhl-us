
-- ItemStatusInsertAuto PROCEDURE
-- Generated 1/18/2008 11:10:47 AM
-- Do not modify the contents of this procedure.
-- Insert Procedure for ItemStatus

CREATE PROCEDURE ItemStatusInsertAuto

@ItemStatusID INT,
@ItemStatusName NVARCHAR(50)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[ItemStatus]
(
	[ItemStatusID],
	[ItemStatusName]
)
VALUES
(
	@ItemStatusID,
	@ItemStatusName
)

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemStatusInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[ItemStatusID],
		[ItemStatusName]	

	FROM [dbo].[ItemStatus]
	
	WHERE
		[ItemStatusID] = @ItemStatusID
	
	RETURN -- insert successful
END

