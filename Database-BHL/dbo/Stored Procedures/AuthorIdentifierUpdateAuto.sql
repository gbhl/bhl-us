
-- AuthorIdentifierUpdateAuto PROCEDURE
-- Generated 5/18/2012 11:11:49 AM
-- Do not modify the contents of this procedure.
-- Update Procedure for AuthorIdentifier

CREATE PROCEDURE AuthorIdentifierUpdateAuto

@AuthorIdentifierID INT,
@AuthorID INT,
@IdentifierID INT,
@IdentifierValue NVARCHAR(125),
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[AuthorIdentifier]

SET

	[AuthorID] = @AuthorID,
	[IdentifierID] = @IdentifierID,
	[IdentifierValue] = @IdentifierValue,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[AuthorIdentifierID] = @AuthorIdentifierID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AuthorIdentifierUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[AuthorIdentifierID],
		[AuthorID],
		[IdentifierID],
		[IdentifierValue],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]

	FROM [dbo].[AuthorIdentifier]
	
	WHERE
		[AuthorIdentifierID] = @AuthorIdentifierID
	
	RETURN -- update successful
END


