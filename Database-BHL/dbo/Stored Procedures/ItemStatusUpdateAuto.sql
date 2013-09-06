
-- ItemStatusUpdateAuto PROCEDURE
-- Generated 1/18/2008 11:10:47 AM
-- Do not modify the contents of this procedure.
-- Update Procedure for ItemStatus

CREATE PROCEDURE ItemStatusUpdateAuto

@ItemStatusID INT,
@ItemStatusName NVARCHAR(50)

AS 

SET NOCOUNT ON

UPDATE [dbo].[ItemStatus]

SET

	[ItemStatusID] = @ItemStatusID,
	[ItemStatusName] = @ItemStatusName

WHERE
	[ItemStatusID] = @ItemStatusID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ItemStatusUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[ItemStatusID],
		[ItemStatusName]

	FROM [dbo].[ItemStatus]
	
	WHERE
		[ItemStatusID] = @ItemStatusID
	
	RETURN -- update successful
END

