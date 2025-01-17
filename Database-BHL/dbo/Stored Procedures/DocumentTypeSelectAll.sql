CREATE PROCEDURE [dbo].[DocumentTypeSelectAll]

AS 

SET NOCOUNT ON

SELECT	[DocumentTypeID],
		[Name],
		[Label],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]
FROM	[dbo].[DocumentType]
ORDER BY [Label]

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure DocumentTypeSelectAll. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
