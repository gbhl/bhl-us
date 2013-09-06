
-- AuthorTypeUpdateAuto PROCEDURE
-- Generated 5/18/2012 11:11:49 AM
-- Do not modify the contents of this procedure.
-- Update Procedure for AuthorType

CREATE PROCEDURE AuthorTypeUpdateAuto

@AuthorTypeID INT,
@AuthorTypeName NVARCHAR(50),
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[AuthorType]

SET

	[AuthorTypeName] = @AuthorTypeName,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[AuthorTypeID] = @AuthorTypeID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AuthorTypeUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[AuthorTypeID],
		[AuthorTypeName],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]

	FROM [dbo].[AuthorType]
	
	WHERE
		[AuthorTypeID] = @AuthorTypeID
	
	RETURN -- update successful
END


