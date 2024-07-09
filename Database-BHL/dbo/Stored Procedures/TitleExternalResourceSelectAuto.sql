CREATE PROCEDURE [dbo].[TitleExternalResourceSelectAuto]

@TitleExternalResourceID INT

AS 

SET NOCOUNT ON

SELECT	
	[TitleExternalResourceID],
	[TitleID],
	[ExternalResourceTypeID],
	[UrlText],
	[Url],
	[SequenceOrder],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
FROM	
	[dbo].[TitleExternalResource]
WHERE	
	[TitleExternalResourceID] = @TitleExternalResourceID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.TitleExternalResourceSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
