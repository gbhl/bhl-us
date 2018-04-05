CREATE PROCEDURE srchindex.SearchCatalogSegmentInsertUpdate

@SegmentID int,
@ItemID int,
@Title nvarchar(2000),
@TranslatedTitle nvarchar(2000),
@ContainerTitle nvarchar(2000),
@PublicationDetails nvarchar(400),
@Volume nvarchar(100),
@Series nvarchar(100),
@Issue nvarchar(100),
@Date nvarchar(20),
@Subjects nvarchar(max),
@Authors nvarchar(max),
@SearchAuthors nvarchar(max),
@Contributors nvarchar(max),
@HasLocalContent smallint,
@HasExternalContent smallint,
@HasIllustrations smallint

AS

BEGIN

SET NOCOUNT ON

DECLARE @SearchText nvarchar(4000)

SET @SearchText = LEFT(@Title + ' ' + @ContainerTitle + ' ' + @Volume + ' ' + @Series + ' ' + 
						@Issue + ' ' + @Date + ' ' + @Subjects + ' ' + @SearchAuthors, 4000)

IF EXISTS(SELECT SearchCatalogSegmentID FROM dbo.SearchCatalogSegment WHERE SegmentID = @SegmentID)
BEGIN
	UPDATE	dbo.SearchCatalogSegment
	SET		ItemID = @ItemID,
			Title = @Title,
			TranslatedTitle = @TranslatedTitle,
			ContainerTitle = @ContainerTitle,
			PublicationDetails = @PublicationDetails,
			Volume = @Volume,
			Series = @Series,
			Issue = @Issue,
			[Date] = @Date,
			Subjects = @Subjects,
			Authors = @Authors,
			SearchAuthors = @SearchAuthors,
			Contributors = @Contributors,
			HasLocalContent = @HasLocalContent,
			HasExternalContent = @HasExternalContent,
			HasIllustrations = @HasIllustrations,
			SearchText = @SearchText,
			LastModifiedDate = GETDATE()
	WHERE	SegmentID = @SegmentID
END
ELSE
BEGIN
	INSERT	dbo.SearchCatalogSegment (SegmentID, ItemID, Title, TranslatedTitle, ContainerTitle, PublicationDetails,
		Volume, Series, Issue, [Date], Subjects, Authors, SearchAuthors, Contributors, HasLocalContent, 
		HasExternalContent, HasIllustrations, SearchText)
	VALUES	(@SegmentID, @ItemID, @Title, @TranslatedTitle, @ContainerTitle, @PublicationDetails,
		@Volume, @Series, @Issue, @Date, @Subjects, @Authors, @SearchAuthors, @Contributors, @HasLocalContent, 
		@HasExternalContent, @HasIllustrations, @SearchText)
END

END
