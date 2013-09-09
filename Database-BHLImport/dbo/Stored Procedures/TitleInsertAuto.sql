
-- TitleInsertAuto PROCEDURE
-- Generated 5/19/2009 10:35:29 AM
-- Do not modify the contents of this procedure.
-- Insert Procedure for Title

CREATE PROCEDURE TitleInsertAuto

@TitleID INT OUTPUT,
@ImportKey NVARCHAR(50),
@ImportStatusID INT,
@ImportSourceID INT = null,
@MARCBibID NVARCHAR(50),
@MARCLeader NVARCHAR(24) = null,
@FullTitle NTEXT = null,
@ShortTitle NVARCHAR(255) = null,
@UniformTitle NVARCHAR(255) = null,
@SortTitle NVARCHAR(60) = null,
@PartNumber NVARCHAR(255) = null,
@PartName NVARCHAR(255) = null,
@CallNumber NVARCHAR(100) = null,
@PublicationDetails NVARCHAR(255) = null,
@StartYear SMALLINT = null,
@EndYear SMALLINT = null,
@Datafield_260_a NVARCHAR(150) = null,
@Datafield_260_b NVARCHAR(255) = null,
@Datafield_260_c NVARCHAR(100) = null,
@InstitutionCode NVARCHAR(10) = null,
@LanguageCode NVARCHAR(10) = null,
@TitleDescription NTEXT = null,
@TL2Author NVARCHAR(100) = null,
@PublishReady BIT = null,
@RareBooks BIT = null,
@OriginalCatalogingSource NVARCHAR(100) = null,
@EditionStatement NVARCHAR(450) = null,
@CurrentPublicationFrequency NVARCHAR(100) = null,
@Note NVARCHAR(255) = null,
@ExternalCreationDate DATETIME = null,
@ExternalLastModifiedDate DATETIME = null,
@ExternalCreationUser INT = null,
@ExternalLastModifiedUser INT = null,
@ProductionDate DATETIME = null,
@ProductionTitleID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Title]
(
	[ImportKey],
	[ImportStatusID],
	[ImportSourceID],
	[MARCBibID],
	[MARCLeader],
	[FullTitle],
	[ShortTitle],
	[UniformTitle],
	[SortTitle],
	[PartNumber],
	[PartName],
	[CallNumber],
	[PublicationDetails],
	[StartYear],
	[EndYear],
	[Datafield_260_a],
	[Datafield_260_b],
	[Datafield_260_c],
	[InstitutionCode],
	[LanguageCode],
	[TitleDescription],
	[TL2Author],
	[PublishReady],
	[RareBooks],
	[OriginalCatalogingSource],
	[EditionStatement],
	[CurrentPublicationFrequency],
	[Note],
	[ExternalCreationDate],
	[ExternalLastModifiedDate],
	[ExternalCreationUser],
	[ExternalLastModifiedUser],
	[ProductionDate],
	[ProductionTitleID],
	[CreatedDate],
	[LastModifiedDate]
)
VALUES
(
	@ImportKey,
	@ImportStatusID,
	@ImportSourceID,
	@MARCBibID,
	@MARCLeader,
	@FullTitle,
	@ShortTitle,
	@UniformTitle,
	@SortTitle,
	@PartNumber,
	@PartName,
	@CallNumber,
	@PublicationDetails,
	@StartYear,
	@EndYear,
	@Datafield_260_a,
	@Datafield_260_b,
	@Datafield_260_c,
	@InstitutionCode,
	@LanguageCode,
	@TitleDescription,
	@TL2Author,
	@PublishReady,
	@RareBooks,
	@OriginalCatalogingSource,
	@EditionStatement,
	@CurrentPublicationFrequency,
	@Note,
	@ExternalCreationDate,
	@ExternalLastModifiedDate,
	@ExternalCreationUser,
	@ExternalLastModifiedUser,
	@ProductionDate,
	@ProductionTitleID,
	getdate(),
	getdate()
)

SET @TitleID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[TitleID],
		[ImportKey],
		[ImportStatusID],
		[ImportSourceID],
		[MARCBibID],
		[MARCLeader],
		[FullTitle],
		[ShortTitle],
		[UniformTitle],
		[SortTitle],
		[PartNumber],
		[PartName],
		[CallNumber],
		[PublicationDetails],
		[StartYear],
		[EndYear],
		[Datafield_260_a],
		[Datafield_260_b],
		[Datafield_260_c],
		[InstitutionCode],
		[LanguageCode],
		[TitleDescription],
		[TL2Author],
		[PublishReady],
		[RareBooks],
		[OriginalCatalogingSource],
		[EditionStatement],
		[CurrentPublicationFrequency],
		[Note],
		[ExternalCreationDate],
		[ExternalLastModifiedDate],
		[ExternalCreationUser],
		[ExternalLastModifiedUser],
		[ProductionDate],
		[ProductionTitleID],
		[CreatedDate],
		[LastModifiedDate]	

	FROM [dbo].[Title]
	
	WHERE
		[TitleID] = @TitleID
	
	RETURN -- insert successful
END

