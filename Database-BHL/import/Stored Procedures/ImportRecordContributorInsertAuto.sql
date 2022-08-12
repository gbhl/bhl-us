CREATE PROCEDURE import.ImportRecordContributorInsertAuto

@ImportRecordContributorID INT OUTPUT,
@ImportRecordID INT,
@InstitutionCode NVARCHAR(10),
@CreationUserID INT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

INSERT INTO [import].[ImportRecordContributor]
( 	[ImportRecordID],
	[InstitutionCode],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID] )
VALUES
( 	@ImportRecordID,
	@InstitutionCode,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID )

SET @ImportRecordContributorID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure import.ImportRecordContributorInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[ImportRecordContributorID],
		[ImportRecordID],
		[InstitutionCode],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	
	FROM [import].[ImportRecordContributor]
	WHERE
		[ImportRecordContributorID] = @ImportRecordContributorID
	
	RETURN -- insert successful
END
GO
