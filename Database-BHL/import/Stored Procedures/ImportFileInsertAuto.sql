CREATE PROCEDURE import.ImportFileInsertAuto

@ImportFileID INT OUTPUT,
@ImportFileStatusID INT,
@ImportFileName NVARCHAR(200),
@ContributorCode NVARCHAR(10),
@CreationUserID INT,
@LastModifiedUserID INT,
@SegmentGenreID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [import].[ImportFile]
( 	[ImportFileStatusID],
	[ImportFileName],
	[ContributorCode],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID],
	[SegmentGenreID] )
VALUES
( 	@ImportFileStatusID,
	@ImportFileName,
	@ContributorCode,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID,
	@SegmentGenreID )

SET @ImportFileID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure import.ImportFileInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[ImportFileID],
		[ImportFileStatusID],
		[ImportFileName],
		[ContributorCode],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID],
		[SegmentGenreID]	
	FROM [import].[ImportFile]
	WHERE
		[ImportFileID] = @ImportFileID
	
	RETURN -- insert successful
END
