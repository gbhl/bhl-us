CREATE PROCEDURE dbo.DOIInsertAuto

@DOIID INT OUTPUT,
@DOIEntityTypeID INT,
@EntityID INT,
@DOIStatusID INT,
@DOIBatchID NVARCHAR(50),
@DOIName NVARCHAR(50),
@StatusDate DATETIME,
@StatusMessage NVARCHAR(1000),
@IsValid SMALLINT,
@CreationUserID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[DOI]
( 	[DOIEntityTypeID],
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
	[LastModifiedUserID] )
VALUES
( 	@DOIEntityTypeID,
	@EntityID,
	@DOIStatusID,
	@DOIBatchID,
	@DOIName,
	@StatusDate,
	@StatusMessage,
	@IsValid,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID )

SET @DOIID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.DOIInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END
GO
