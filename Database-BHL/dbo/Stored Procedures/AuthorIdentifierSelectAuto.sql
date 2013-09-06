
-- AuthorIdentifierSelectAuto PROCEDURE
-- Generated 5/18/2012 11:11:49 AM
-- Do not modify the contents of this procedure.
-- Select Procedure for AuthorIdentifier

CREATE PROCEDURE AuthorIdentifierSelectAuto

@AuthorIdentifierID INT

AS 

SET NOCOUNT ON

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

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AuthorIdentifierSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


