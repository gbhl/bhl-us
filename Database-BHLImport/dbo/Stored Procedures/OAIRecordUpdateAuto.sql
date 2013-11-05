CREATE PROCEDURE OAIRecordUpdateAuto

@OAIRecordID INT,
@HarvestLogID INT,
@OAIIdentifier NVARCHAR(100),
@OAIDateStamp NVARCHAR(30),
@OAIStatus NVARCHAR(20),
@RecordType NVARCHAR(20),
@Title NVARCHAR(2000),
@ContainerTitle NVARCHAR(2000),
@Contributor NVARCHAR(200),
@Date NVARCHAR(20),
@Language NVARCHAR(30),
@Publisher NVARCHAR(250),
@PublicationPlace NVARCHAR(150),
@PublicationDate NVARCHAR(100),
@Edition NVARCHAR(450),
@Volume NVARCHAR(100),
@Issue NVARCHAR(100),
@StartPage NVARCHAR(20),
@EndPage NVARCHAR(20),
@CallNumber NVARCHAR(100),
@Issn NVARCHAR(125),
@Isbn NVARCHAR(125),
@Lccn NVARCHAR(125),
@Doi NVARCHAR(50),
@Url NVARCHAR(200),
@OAIRecordStatusID INT,
@ProductionEntityType NVARCHAR(5),
@ProductionEntityID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[OAIRecord]

SET

	[HarvestLogID] = @HarvestLogID,
	[OAIIdentifier] = @OAIIdentifier,
	[OAIDateStamp] = @OAIDateStamp,
	[OAIStatus] = @OAIStatus,
	[RecordType] = @RecordType,
	[Title] = @Title,
	[ContainerTitle] = @ContainerTitle,
	[Contributor] = @Contributor,
	[Date] = @Date,
	[Language] = @Language,
	[Publisher] = @Publisher,
	[PublicationPlace] = @PublicationPlace,
	[PublicationDate] = @PublicationDate,
	[Edition] = @Edition,
	[Volume] = @Volume,
	[Issue] = @Issue,
	[StartPage] = @StartPage,
	[EndPage] = @EndPage,
	[CallNumber] = @CallNumber,
	[Issn] = @Issn,
	[Isbn] = @Isbn,
	[Lccn] = @Lccn,
	[Doi] = @Doi,
	[Url] = @Url,
	[OAIRecordStatusID] = @OAIRecordStatusID,
	[ProductionEntityType] = @ProductionEntityType,
	[ProductionEntityID] = @ProductionEntityID,
	[LastModifiedDate] = getdate()

WHERE
	[OAIRecordID] = @OAIRecordID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure OAIRecordUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- update successful
END

GO
