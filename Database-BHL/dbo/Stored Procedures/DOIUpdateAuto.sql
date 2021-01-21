CREATE PROCEDURE dbo.DOIUpdateAuto

@DOIID INT,
@DOIEntityTypeID INT,
@EntityID INT,
@DOIStatusID INT,
@DOIBatchID NVARCHAR(50),
@DOIName NVARCHAR(50),
@StatusDate DATETIME,
@StatusMessage NVARCHAR(1000),
@IsValid SMALLINT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[DOI]
SET
	[DOIEntityTypeID] = @DOIEntityTypeID,
	[EntityID] = @EntityID,
	[DOIStatusID] = @DOIStatusID,
	[DOIBatchID] = @DOIBatchID,
	[DOIName] = @DOIName,
	[StatusDate] = @StatusDate,
	[StatusMessage] = @StatusMessage,
	[IsValid] = @IsValid,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID
WHERE
	[DOIID] = @DOIID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.DOIUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	FROM [dbo].[DOI]
	WHERE
		[DOIID] = @DOIID
	
	RETURN -- update successful
END
GO
