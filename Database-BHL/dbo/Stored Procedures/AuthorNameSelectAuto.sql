
-- AuthorNameSelectAuto PROCEDURE
-- Generated 5/29/2012 12:59:27 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for AuthorName

CREATE PROCEDURE AuthorNameSelectAuto

@AuthorNameID INT

AS 

SET NOCOUNT ON

SELECT 

	[AuthorNameID],
	[AuthorID],
	[FullName],
	[LastName],
	[FirstName],
	[FullerForm],
	[IsPreferredName],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]

FROM [dbo].[AuthorName]

WHERE
	[AuthorNameID] = @AuthorNameID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AuthorNameSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


