CREATE PROCEDURE dbo.TitleUpdateAuto

@TitleID INT,
@MARCBibID NVARCHAR(50),
@MARCLeader NVARCHAR(24),
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
@LanguageCode NVARCHAR(10),
@TitleDescription NTEXT,
@TL2Author NVARCHAR(100),
@PublishReady BIT,
@RareBooks BIT,
@Note NVARCHAR(MAX),
@LastModifiedUserID INT,
@OriginalCatalogingSource NVARCHAR(100),
@EditionStatement NVARCHAR(450),
@CurrentPublicationFrequency NVARCHAR(100),
@PartNumber NVARCHAR(255),
@PartName NVARCHAR(255),
@BibliographicLevelID INT,
@MaterialTypeID INT,
@HasMovingWall SMALLINT

AS 

SET NOCOUNT ON

UPDATE [dbo].[Title]
SET
	[MARCBibID] = @MARCBibID,
	[MARCLeader] = @MARCLeader,
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
	[PartName] = @PartName,
	[BibliographicLevelID] = @BibliographicLevelID,
	[MaterialTypeID] = @MaterialTypeID,
	[HasMovingWall] = @HasMovingWall
WHERE
	[TitleID] = @TitleID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.TitleUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[TitleID],
		[MARCBibID],
		[MARCLeader],
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
		[PartName],
		[BibliographicLevelID],
		[MaterialTypeID],
		[HasMovingWall]
	FROM [dbo].[Title]
	WHERE
		[TitleID] = @TitleID
	
	RETURN -- update successful
END
GO
