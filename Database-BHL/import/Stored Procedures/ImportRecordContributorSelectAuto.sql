CREATE PROCEDURE [import].[ImportRecordContributorSelectAuto]

@ImportRecordContributorID INT

AS 

SET NOCOUNT ON

SELECT	
	[ImportRecordContributorID],
	[ImportRecordID],
	[InstitutionCode],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
FROM	
	[import].[ImportRecordContributor]
WHERE	
	[ImportRecordContributorID] = @ImportRecordContributorID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure import.ImportRecordContributorSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
