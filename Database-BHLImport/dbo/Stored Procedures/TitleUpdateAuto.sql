
-- TitleUpdateAuto PROCEDURE
-- Generated 5/19/2009 10:35:29 AM
-- Do not modify the contents of this procedure.
-- Update Procedure for Title

CREATE PROCEDURE TitleUpdateAuto

@TitleID INT,
@ImportKey NVARCHAR(50),
@ImportStatusID INT,
@ImportSourceID INT,
@MARCBibID NVARCHAR(50),
@MARCLeader NVARCHAR(24),
@FullTitle NTEXT,
@ShortTitle NVARCHAR(255),
@UniformTitle NVARCHAR(255),
@SortTitle NVARCHAR(60),
@PartNumber NVARCHAR(255),
@PartName NVARCHAR(255),
@CallNumber NVARCHAR(100),
@PublicationDetails NVARCHAR(255),
@StartYear SMALLINT,
@EndYear SMALLINT,
@Datafield_260_a NVARCHAR(150),
@Datafield_260_b NVARCHAR(255),
@Datafield_260_c NVARCHAR(100),
@InstitutionCode NVARCHAR(10),
@LanguageCode NVARCHAR(10),
@TitleDescription NTEXT,
@TL2Author NVARCHAR(100),
@PublishReady BIT,
@RareBooks BIT,
@OriginalCatalogingSource NVARCHAR(100),
@EditionStatement NVARCHAR(450),
@CurrentPublicationFrequency NVARCHAR(100),
@Note NVARCHAR(255),
@ExternalCreationDate DATETIME,
@ExternalLastModifiedDate DATETIME,
@ExternalCreationUser INT,
@ExternalLastModifiedUser INT,
@ProductionDate DATETIME,
@ProductionTitleID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[Title]

SET

	[ImportKey] = @ImportKey,
	[ImportStatusID] = @ImportStatusID,
	[ImportSourceID] = @ImportSourceID,
	[MARCBibID] = @MARCBibID,
	[MARCLeader] = @MARCLeader,
	[FullTitle] = @FullTitle,
	[ShortTitle] = @ShortTitle,
	[UniformTitle] = @UniformTitle,
	[SortTitle] = @SortTitle,
	[PartNumber] = @PartNumber,
	[PartName] = @PartName,
	[CallNumber] = @CallNumber,
	[PublicationDetails] = @PublicationDetails,
	[StartYear] = @StartYear,
	[EndYear] = @EndYear,
	[Datafield_260_a] = @Datafield_260_a,
	[Datafield_260_b] = @Datafield_260_b,
	[Datafield_260_c] = @Datafield_260_c,
	[InstitutionCode] = @InstitutionCode,
	[LanguageCode] = @LanguageCode,
	[TitleDescription] = @TitleDescription,
	[TL2Author] = @TL2Author,
	[PublishReady] = @PublishReady,
	[RareBooks] = @RareBooks,
	[OriginalCatalogingSource] = @OriginalCatalogingSource,
	[EditionStatement] = @EditionStatement,
	[CurrentPublicationFrequency] = @CurrentPublicationFrequency,
	[Note] = @Note,
	[ExternalCreationDate] = @ExternalCreationDate,
	[ExternalLastModifiedDate] = @ExternalLastModifiedDate,
	[ExternalCreationUser] = @ExternalCreationUser,
	[ExternalLastModifiedUser] = @ExternalLastModifiedUser,
	[ProductionDate] = @ProductionDate,
	[ProductionTitleID] = @ProductionTitleID,
	[LastModifiedDate] = getdate()

WHERE
	[TitleID] = @TitleID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

