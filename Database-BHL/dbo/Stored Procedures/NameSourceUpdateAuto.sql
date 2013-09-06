
-- NameSourceUpdateAuto PROCEDURE
-- Generated 9/18/2012 11:59:15 AM
-- Do not modify the contents of this procedure.
-- Update Procedure for NameSource

CREATE PROCEDURE NameSourceUpdateAuto

@NameSourceID INT,
@SourceName NVARCHAR(50),
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[NameSource]

SET

	[SourceName] = @SourceName,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[NameSourceID] = @NameSourceID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure NameSourceUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[NameSourceID],
		[SourceName],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]

	FROM [dbo].[NameSource]
	
	WHERE
		[NameSourceID] = @NameSourceID
	
	RETURN -- update successful
END

