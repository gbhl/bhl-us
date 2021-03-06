CREATE PROCEDURE [import].[ImportFileStatusInsertAuto]

@ImportFileStatusID INT,
@StatusName NVARCHAR(50),
@StatusDescription NVARCHAR(500),
@CreationUserID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

INSERT INTO [import].[ImportFileStatus]
(
	[ImportFileStatusID],
	[StatusName],
	[StatusDescription],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@ImportFileStatusID,
	@StatusName,
	@StatusDescription,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ImportFileStatusInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[ImportFileStatusID],
		[StatusName],
		[StatusDescription],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	

	FROM [import].[ImportFileStatus]
	
	WHERE
		[ImportFileStatusID] = @ImportFileStatusID
	
	RETURN -- insert successful
END


GO
