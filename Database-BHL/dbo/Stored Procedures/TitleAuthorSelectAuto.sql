
-- TitleAuthorSelectAuto PROCEDURE
-- Generated 5/29/2012 12:59:27 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for TitleAuthor

CREATE PROCEDURE TitleAuthorSelectAuto

@TitleAuthorID INT

AS 

SET NOCOUNT ON

SELECT 

	[TitleAuthorID],
	[TitleID],
	[AuthorID],
	[AuthorRoleID],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]

FROM [dbo].[TitleAuthor]

WHERE
	[TitleAuthorID] = @TitleAuthorID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleAuthorSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


