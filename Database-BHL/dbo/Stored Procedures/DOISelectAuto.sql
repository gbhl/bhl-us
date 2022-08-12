CREATE PROCEDURE [dbo].[DOISelectAuto]

@DOIID INT

AS 

SET NOCOUNT ON

SELECT	
	[DOIID],
	[DOIEntityTypeID],
	[EntityID],
	[DOIStatusID],
	[DOIBatchID],
	[DOIName],
	[StatusDate],
	[StatusMessage],
	[IsValid],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
FROM	
	[dbo].[DOI]
WHERE	
	[DOIID] = @DOIID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.DOISelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
