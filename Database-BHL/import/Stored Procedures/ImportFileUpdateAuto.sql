CREATE PROCEDURE import.ImportFileUpdateAuto

@ImportFileID INT,
@ImportFileStatusID INT,
@ImportFileName NVARCHAR(200),
@ContributorCode NVARCHAR(10),
@SegmentGenreID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [import].[ImportFile]
SET
	[ImportFileStatusID] = @ImportFileStatusID,
	[ImportFileName] = @ImportFileName,
	[ContributorCode] = @ContributorCode,
	[SegmentGenreID] = @SegmentGenreID,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID
WHERE
	[ImportFileID] = @ImportFileID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure import.ImportFileUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	FROM [import].[ImportFile]
	WHERE
		[ImportFileID] = @ImportFileID
	
	RETURN -- update successful
END
