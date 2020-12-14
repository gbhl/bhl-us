CREATE PROCEDURE [dbo].[PageTypeSelectAuto]

@PageTypeID INT

AS 

SET NOCOUNT ON

SELECT	
	[PageTypeID],
	[PageTypeName],
	[PageTypeDescription],
	[Active],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
FROM	
	[dbo].[PageType]
WHERE	
	[PageTypeID] = @PageTypeID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.PageTypeSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
