CREATE PROCEDURE [dbo].[DOIInsert]

@DOIEntityTypeID INT,
@EntityID INT,
@DOIStatusID INT,
@DOIBatchID NVARCHAR(50),
@DOIName NVARCHAR(50),
@StatusMessage NVARCHAR(1000),
@IsValid SMALLINT,
@CreationUserID INT,
@LastModifiedUserID INT,
@AllowDuplicate SMALLINT

AS 

SET NOCOUNT ON

IF NOT EXISTS(	SELECT	DOIID 
				FROM	dbo.DOI 
				WHERE	DOIEntityTypeID = @DOIEntityTypeID 
				AND		EntityID = @EntityID
			) 
	OR @AllowDuplicate = 1
BEGIN
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
		getdate(),
		@StatusMessage,
		@IsValid,
		getdate(),
		getdate(),
		@CreationUserID,
		@LastModifiedUserID )
END

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.DOIInsert. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- insert successful
END

GO
