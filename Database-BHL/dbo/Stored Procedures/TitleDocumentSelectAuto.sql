CREATE PROCEDURE [dbo].[TitleDocumentSelectAuto]

@TitleDocumentID INT

AS 

SET NOCOUNT ON

SELECT	
	[TitleDocumentID],
	[TitleID],
	[DocumentTypeID],
	[Name],
	[Url],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
FROM	
	[dbo].[TitleDocument]
WHERE	
	[TitleDocumentID] = @TitleDocumentID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.TitleDocumentSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
