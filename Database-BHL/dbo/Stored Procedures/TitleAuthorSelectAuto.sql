CREATE PROCEDURE [dbo].[TitleAuthorSelectAuto]

@TitleAuthorID INT

AS 

SET NOCOUNT ON

SELECT	
	[TitleAuthorID],
	[TitleID],
	[AuthorID],
	[AuthorRoleID],
	[Relationship],
	[TitleOfWork],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID],
	[SequenceOrder]
FROM	
	[dbo].[TitleAuthor]
WHERE	
	[TitleAuthorID] = @TitleAuthorID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.TitleAuthorSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
