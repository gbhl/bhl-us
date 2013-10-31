CREATE PROCEDURE OAIRecordInsertAuto

@OAIRecordID INT OUTPUT,
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
@Issn NVARCHAR(125),
@Isbn NVARCHAR(125),
@Lccn NVARCHAR(125),
@Doi NVARCHAR(50),
@Url NVARCHAR(200),
@OAIRecordStatusID INT,
@ProductionEntityType NVARCHAR(5) = null,
@ProductionEntityID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[OAIRecord]
(
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
)
VALUES
(
	@HarvestLogID,
	@OAIIdentifier,
	@OAIDateStamp,
	@OAIStatus,
	@RecordType,
	@Title,
	@ContainerTitle,
	@Contributor,
	@Date,
	@Language,
	@Publisher,
	@PublicationPlace,
	@PublicationDate,
	@Edition,
	@Volume,
	@Issue,
	@StartPage,
	@EndPage,
	@Issn,
	@Isbn,
	@Lccn,
	@Doi,
	@Url,
	@OAIRecordStatusID,
	@ProductionEntityType,
	@ProductionEntityID,
	getdate(),
	getdate()
)

SET @OAIRecordID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure OAIRecordInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END
