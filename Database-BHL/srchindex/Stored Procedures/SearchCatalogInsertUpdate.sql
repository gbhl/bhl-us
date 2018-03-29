CREATE PROCEDURE srchindex.SearchCatalogInsertUpdate

@TitleID int,
@ItemID int,
@FullTitle nvarchar(2000),
@UniformTitle nvarchar(255),
@PublicationDetails nvarchar(255),
@PublisherPlace nvarchar(150),
@PublisherName nvarchar(255),
@Volume nvarchar(100),
@EditionStatement nvarchar(450),
@Dates nvarchar(100),
@Subjects nvarchar(max),
@Associations nvarchar(max),
@Variants nvarchar(max),
@Authors nvarchar(max),
@SearchAuthors nvarchar(max),
@TitleContributors nvarchar(max),
@ItemContributors nvarchar(max),
@FirstPageID int,
@HasSegments smallint,
@HasLocalContent smallint,
@HasExternalContent smallint,
@HasIllustrations smallint

AS

BEGIN

SET NOCOUNT ON

DECLARE @SearchText nvarchar(4000)

SET @SearchText = LEFT(@FullTitle + ' ' +
				@UniformTitle + ' ' +
				@Volume + ' ' +
				@Dates + ' ' +
				@Subjects + ' ' +
				@Associations + ' ' +
				@SearchAuthors + ' ' +
				@Variants, 4000)

IF EXISTS(SELECT SearchCatalogID FROM dbo.SearchCatalog WHERE TitleID = @TitleID AND ItemID = @ItemID)
BEGIN
	UPDATE	dbo.SearchCatalog
	SET		FullTitle = @FullTitle,
			UniformTitle = @UniformTitle,
			PublicationDetails = @PublicationDetails,
			PublisherPlace = @PublisherPlace,
			PublisherName = @PublisherName,
			Volume = @Volume,
			EditionStatement = @EditionStatement,
			Subjects = @Subjects,
			Associations = @Associations,
			Variants = @Variants,
			Authors = @Authors,
			SearchAuthors = @SearchAuthors,
			TitleContributors = @TitleContributors,
			ItemContributors = @ItemContributors,
			FirstPageID = @FirstPageID,
			HasSegments = @HasSegments,
			HasLocalContent = @HasLocalContent,
			HasExternalContent = @HasExternalContent,
			HasIllustrations = @HasIllustrations,
			SearchText = @SearchText
	WHERE	TitleID = @TitleID
	AND		ItemID = @ItemID
END
ELSE
BEGIN
	INSERT	dbo.SearchCatalog (TitleID, ItemID, FullTitle, UniformTitle, PublisherPlace,
		PublisherName, Volume, Subjects, Associations, Variants, Authors, SearchAuthors,
		PublicationDetails, TitleContributors, EditionStatement, FirstPageID, 
		ItemContributors, HasSegments, HasLocalContent, HasExternalContent,
		HasIllustrations, SearchText)
	VALUES	(@TitleID, @ItemID, @FullTitle, @UniformTitle, @PublisherPlace,
		@PublisherName, @Volume, @Subjects, @Associations, @Variants, @Authors, @SearchAuthors,
		@PublicationDetails, @TitleContributors, @EditionStatement, @FirstPageID, 
		@ItemContributors, @HasSegments, @HasLocalContent, @HasExternalContent,
		@HasIllustrations, @SearchText)
END

END
