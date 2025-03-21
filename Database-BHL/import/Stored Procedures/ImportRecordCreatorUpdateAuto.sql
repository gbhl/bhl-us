CREATE PROCEDURE import.ImportRecordCreatorUpdateAuto

@ImportRecordCreatorID INT,
@ImportRecordID INT,
@FullName NVARCHAR(300),
@FirstName NVARCHAR(150),
@LastName NVARCHAR(150),
@StartYear NVARCHAR(25),
@EndYear NVARCHAR(25),
@AuthorType NVARCHAR(50),
@LastModifiedUserID INT,
@ProductionAuthorID INT,
@ImportedAuthorID INT

AS 

SET NOCOUNT ON

UPDATE [import].[ImportRecordCreator]
SET
	[ImportRecordID] = @ImportRecordID,
	[FullName] = @FullName,
	[FirstName] = @FirstName,
	[LastName] = @LastName,
	[StartYear] = @StartYear,
	[EndYear] = @EndYear,
	[AuthorType] = @AuthorType,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID,
	[ProductionAuthorID] = @ProductionAuthorID,
	[ImportedAuthorID] = @ImportedAuthorID
WHERE
	[ImportRecordCreatorID] = @ImportRecordCreatorID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure import.ImportRecordCreatorUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END
