CREATE PROCEDURE [import].[ImportFileSelectAuto]

@ImportFileID INT

AS 

SET NOCOUNT ON

SELECT	
	[ImportFileID],
	[ImportFileStatusID],
	[ImportFileName],
	[ContributorCode],
	[SegmentGenreID],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
FROM	
	[import].[ImportFile]
WHERE	
	[ImportFileID] = @ImportFileID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure import.ImportFileSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
