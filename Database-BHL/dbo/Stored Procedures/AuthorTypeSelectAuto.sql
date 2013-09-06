
-- AuthorTypeSelectAuto PROCEDURE
-- Generated 5/18/2012 11:11:49 AM
-- Do not modify the contents of this procedure.
-- Select Procedure for AuthorType

CREATE PROCEDURE AuthorTypeSelectAuto

@AuthorTypeID INT

AS 

SET NOCOUNT ON

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

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AuthorTypeSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


