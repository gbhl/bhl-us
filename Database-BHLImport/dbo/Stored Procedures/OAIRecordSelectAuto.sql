CREATE PROCEDURE OAIRecordSelectAuto

@OAIRecordID INT

AS 

SET NOCOUNT ON

SELECT 

	[OAIRecordID],
	[HarvestLogID],
	[OAIIdentifier],
	[OAIDateStamp],
	[OAIStatus],
	[RecordType],
	[Title],
	[ContainerTitle],
	[Contributor],
	[Date],
	[Language],
	[Publisher],
	[PublicationPlace],
	[PublicationDate],
	[Edition],
	[Volume],
	[Issue],
	[StartPage],
	[EndPage],
	[CallNumber],
	[Issn],
	[Isbn],
	[Lccn],
	[Doi],
	[Url],
	[OAIRecordStatusID],
	[ProductionEntityType],
	[ProductionEntityID],
	[CreationDate],
	[LastModifiedDate]

FROM [dbo].[OAIRecord]

WHERE
	[OAIRecordID] = @OAIRecordID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure OAIRecordSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
