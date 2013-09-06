
-- TitleUpdateAuto PROCEDURE
-- Generated 8/3/2010 11:16:34 AM
-- Do not modify the contents of this procedure.
-- Update Procedure for Title

CREATE PROCEDURE TitleUpdateAuto

@TitleID INT,
@MARCBibID NVARCHAR(50),
@MARCLeader NVARCHAR(24),
@BibliographicLevelID INT,
@TropicosTitleID INT,
@RedirectTitleID INT,
@FullTitle NVARCHAR(2000),
@ShortTitle NVARCHAR(255),
@UniformTitle NVARCHAR(255),
@SortTitle NVARCHAR(60),
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
@Note NVARCHAR(255),
@LastModifiedUserID INT,
@OriginalCatalogingSource NVARCHAR(100),
@EditionStatement NVARCHAR(450),
@CurrentPublicationFrequency NVARCHAR(100),
@PartNumber NVARCHAR(255),
@PartName NVARCHAR(255)

AS 

SET NOCOUNT ON

UPDATE [dbo].[Title]

SET

	[MARCBibID] = @MARCBibID,
	[MARCLeader] = @MARCLeader,
	[BibliographicLevelID] = @BibliographicLevelID,
	[TropicosTitleID] = @TropicosTitleID,
	[RedirectTitleID] = @RedirectTitleID,
	[FullTitle] = @FullTitle,
	[ShortTitle] = @ShortTitle,
	[UniformTitle] = @UniformTitle,
	[SortTitle] = @SortTitle,
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
	[Note] = @Note,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID,
	[OriginalCatalogingSource] = @OriginalCatalogingSource,
	[EditionStatement] = @EditionStatement,
	[CurrentPublicationFrequency] = @CurrentPublicationFrequency,
	[PartNumber] = @PartNumber,
	[PartName] = @PartName

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
		[MARCBibID],
		[MARCLeader],
		[BibliographicLevelID],
		[TropicosTitleID],
		[RedirectTitleID],
		[FullTitle],
		[ShortTitle],
		[UniformTitle],
		[SortTitle],
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
		[Note],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID],
		[OriginalCatalogingSource],
		[EditionStatement],
		[CurrentPublicationFrequency],
		[PartNumber],
		[PartName]

	FROM [dbo].[Title]
	
	WHERE
		[TitleID] = @TitleID
	
	RETURN -- update successful
END

