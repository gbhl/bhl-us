CREATE PROCEDURE import.ImportRecordCreatorInsertAuto

@ImportRecordCreatorID INT OUTPUT,
@ImportRecordID INT,
@FullName NVARCHAR(300),
@FirstName NVARCHAR(150),
@LastName NVARCHAR(150),
@StartYear NVARCHAR(25),
@EndYear NVARCHAR(25),
@AuthorType NVARCHAR(50),
@CreationUserID INT,
@LastModifiedUserID INT,
@ProductionAuthorID INT = null,
@ImportedAuthorID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [import].[ImportRecordCreator]
( 	[ImportRecordID],
	[FullName],
	[FirstName],
	[LastName],
	[StartYear],
	[EndYear],
	[AuthorType],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID],
	[ProductionAuthorID],
	[ImportedAuthorID] )
VALUES
( 	@ImportRecordID,
	@FullName,
	@FirstName,
	@LastName,
	@StartYear,
	@EndYear,
	@AuthorType,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID,
	@ProductionAuthorID,
	@ImportedAuthorID )

SET @ImportRecordCreatorID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure import.ImportRecordCreatorInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[ImportRecordCreatorID],
		[ImportRecordID],
		[FullName],
		[FirstName],
		[LastName],
		[StartYear],
		[EndYear],
		[AuthorType],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID],
		[ProductionAuthorID],
		[ImportedAuthorID]	
	FROM [import].[ImportRecordCreator]
	WHERE
		[ImportRecordCreatorID] = @ImportRecordCreatorID
	
	RETURN -- insert successful
END
