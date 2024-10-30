CREATE PROCEDURE [dbo].[AuthorSelectAuto]

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
	[Note],
	[IsActive],
	[RedirectAuthorID],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID],
	[GenerationalSuffix]
FROM	
	[dbo].[Author]
WHERE	
	[AuthorID] = @AuthorID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.AuthorSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
