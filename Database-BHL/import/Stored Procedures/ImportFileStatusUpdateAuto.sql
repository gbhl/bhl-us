CREATE PROCEDURE [import].[ImportFileStatusUpdateAuto]

@ImportFileStatusID INT,
@StatusName NVARCHAR(50),
@StatusDescription NVARCHAR(500),
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [import].[ImportFileStatus]

SET

	[ImportFileStatusID] = @ImportFileStatusID,
	[StatusName] = @StatusName,
	[StatusDescription] = @StatusDescription,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[ImportFileStatusID] = @ImportFileStatusID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure ImportFileStatusUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END


GO
