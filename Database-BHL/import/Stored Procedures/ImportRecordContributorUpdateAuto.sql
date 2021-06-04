CREATE PROCEDURE import.ImportRecordContributorUpdateAuto

@ImportRecordContributorID INT,
@ImportRecordID INT,
@InstitutionCode NVARCHAR(10),
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [import].[ImportRecordContributor]
SET
	[ImportRecordID] = @ImportRecordID,
	[InstitutionCode] = @InstitutionCode,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID
WHERE
	[ImportRecordContributorID] = @ImportRecordContributorID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure import.ImportRecordContributorUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END
GO
