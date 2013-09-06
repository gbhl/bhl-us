
-- AuthorSelectAuto PROCEDURE
-- Generated 5/29/2012 12:59:27 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for Author

CREATE PROCEDURE AuthorSelectAuto

@AuthorID INT

AS 

SET NOCOUNT ON

SELECT 

	[AuthorID],
	[AuthorTypeID],
	[StartDate],
	[EndDate],
	[Numeration],
	[Title],
	[Unit],
	[Location],
	[IsActive],
	[RedirectAuthorID],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]

FROM [dbo].[Author]

WHERE
	[AuthorID] = @AuthorID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AuthorSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


