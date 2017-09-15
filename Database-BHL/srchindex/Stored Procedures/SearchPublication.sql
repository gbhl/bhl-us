CREATE PROCEDURE srchIndex.SearchPublication

@SearchText		nvarchar(2000) = '',
@StartPage		int = 1,
@PageSize		int = 10

AS 

BEGIN

SET NOCOUNT ON

-- Raise an error if the search catalog is offline
DECLARE @CatalogStatus int
exec @CatalogStatus = dbo.SearchCatalogCheckStatus
IF (@CatalogStatus = 0)
BEGIN
	RAISERROR('Search Catalog offline.', 16, 1)
	RETURN 9 -- error occurred
END

BEGIN TRY

	CREATE TABLE #Sortable
	(
		TitleID int NULL,
		ItemID int NULL,
		SegmentID int NULL,
		ItemSequence int NULL,
		Title nvarchar(2000) NOT NULL DEFAULT(''),
		Authors nvarchar(max) NOT NULL DEFAULT(''),
		SearchAuthors nvarchar(max) NOT NULL DEFAULT(''),
		Subjects nvarchar(max) NOT NULL DEFAULT(''),
		Associations nvarchar(max) NOT NULL DEFAULT(''),
		Variants nvarchar(max) NOT NULL DEFAULT(''),
		Contributors nvarchar(max) NOT NULL DEFAULT(''),
		Volume nvarchar(100) NOT NULL DEFAULT(''),
		PublisherName nvarchar(255) NOT NULL DEFAULT(''),
		PublicationPlace nvarchar(150) NOT NULL DEFAULT(''),
		HasSegments smallint NOT NULL DEFAULT(0),
		HasLocalContent smallint NOT NULL DEFAULT(0),
		HasExternalContent smallint NOT NULL DEFAULT(0),
		HasIllustrations smallint NOT NULL DEFAULT(0),
		UniformTitle nvarchar(255) NOT NULL DEFAULT(''),
		SortTitle nvarchar(2000) NOT NULL DEFAULT(''),
		PartNumber nvarchar(255) NOT NULL DEFAULT(''),
		PartName nvarchar(255) NOT NULL DEFAULT(''),
		LanguageName nvarchar(max) NOT NULL DEFAULT(''),
		Genre nvarchar(max) NOT NULL DEFAULT(''),
		MaterialTypeLabel nvarchar(max) NOT NULL DEFAULT(''),
		DOIName nvarchar(max) NOT NULL DEFAULT(''),
		Url nvarchar(max) NOT NULL DEFAULT(''),
		OCLC nvarchar(max) NOT NULL DEFAULT(''),
		ISSN nvarchar(max) NOT NULL DEFAULT(''),
		ISBN nvarchar(max) NOT NULL DEFAULT(''),
		Collections nvarchar(max) NOT NULL DEFAULT(''),
		ContainerTitle nvarchar(300) NOT NULL DEFAULT(''),
		StartPageID int NULL,
		PageRange nvarchar(50) NOT NULL DEFAULT(''),
		StartPageNumber nvarchar(20) NOT NULL DEFAULT(''),
		EndPageNumber nvarchar(20) NOT NULL DEFAULT(''),
		[Date] nvarchar(20) NOT NULL DEFAULT(''),
		Score int NULL
	)	

	DECLARE @Search nvarchar(4000)

	-- Transform the search terms into full-text search phrases
	SELECT @Search = dbo.fnGetFullTextSearchString(@SearchText)

	--############## ITEM ##############

	-- Select items that match the search text
	SELECT	c.TitleID, 
			c.ItemID,
			ti.ItemSequence,
			x.[RANK]
	INTO	#tmpItem
	FROM	CONTAINSTABLE(SearchCatalog, (SearchText), @Search) x
			INNER JOIN SearchCatalog c WITH (NOLOCK) ON c.SearchCatalogID = x.[KEY]
			INNER JOIN dbo.Item i WITH (NOLOCK) ON c.ItemID = i.ItemID
			INNER JOIN dbo.Title t WITH (NOLOCK) ON c.TitleID = t.TitleID
			INNER JOIN dbo.TitleItem ti WITH (NOLOCK) on c.TitleID = ti.TitleID AND c.ItemID = ti.ItemID

	----------------------------------------------------------
	-- Compile the final sortable result set.
	INSERT #Sortable (TitleID, ItemID, ItemSequence, Title, Authors, SearchAuthors, Subjects, Associations,
		Variants, Contributors, Volume, PublisherName, PublicationPlace, HasSegments, HasLocalContent,
		HasExternalContent, HasIllustrations, UniformTitle, SortTitle, PartNumber, PartName, LanguageName,
		Genre, MaterialTypeLabel, DOIName, Url, OCLC, ISSN, ISBN, Collections, [Date], Score)
	SELECT	tmp.TitleID,
			tmp.ItemID,
			tmp.ItemSequence,
			t.FullTitle AS Title,
			c.Authors,
			c.SearchAuthors,
			c.Subjects,
			dbo.fnSeriesStringForTitle (tmp.TitleID) AS Associations,
			c.Variants,
			c.ItemContributors AS Contributors,
			ISNULL(i.Volume, '') AS Volume,
			ISNULL(t.Datafield_260_b, '') AS PublisherName,
			ISNULL(t.Datafield_260_a, '') AS PublicationPlace,
			c.HasSegments,
			c.HasLocalContent,
			c.HasExternalContent,
			c.HasIllustrations,
			ISNULL(t.UniformTitle, '') AS UniformTitle,
			t.SortTitle,
			ISNULL(t.PartNumber, ''),
			ISNULL(t.PartName, ''),
            ISNULL(l.LanguageName, '') AS LanguageName,
            ISNULL(b.BibliographicLevelName, '') AS Genre,
            ISNULL(m.MaterialTypeLabel, '') AS MaterialTypeLabel,
            ISNULL(d.DOIName, '') AS DOIName,
			ISNULL(i.ExternalUrl, '') AS Url,
            dbo.fnGetIdentifierStringForTitle(t.TitleID, 'OCLC') AS OCLC,
            dbo.fnGetIdentifierStringForTitle(t.TitleID, 'ISSN') AS ISSN,
            dbo.fnGetIdentifierStringForTitle(t.TitleID, 'ISBN') AS ISBN,
			dbo.fnCollectionStringForTitleAndItem(tmp.TitleID, i.ItemID) AS Collections,
            ISNULL(CASE WHEN ISNULL(i.[Year], '') = '' THEN CONVERT(nvarchar(20), t.StartYear) ELSE i.[Year] END, '') as [Date],
			tmp.[RANK] AS Score
	FROM	#tmpItem tmp 
			INNER JOIN dbo.Title t WITH (NOLOCK) ON tmp.TitleID = t.TitleID
			INNER JOIN dbo.Item i WITH (NOLOCK) ON tmp.ItemID = i.ItemID
			INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND i.ItemID = c.ItemID
            LEFT JOIN dbo.[Language] l WITH (NOLOCK) ON i.LanguageCode = l.LanguageCode
            LEFT JOIN dbo.BibliographicLevel b WITH (NOLOCK) ON t.BibliographicLevelID = b.BibliographicLevelID
            LEFT JOIN dbo.MaterialType m WITH(NOLOCK) ON t.MaterialTypeID = m.MaterialTypeID
            LEFT JOIN dbo.DOI d WITH (NOLOCK) ON t.TitleID = d.EntityID AND d.DOIStatusID IN (100, 200) AND d.DOIEntityTypeID = 10

	-- De-emphasize ranking of any items:
	--	1) Contributed by Canadiana.org
	--	2) Without local content
	UPDATE	#Sortable
	SET		Score = Score / 100.00
	FROM	#Sortable t
			INNER JOIN dbo.ItemInstitution ii WITH (NOLOCK) ON t.ItemID = ii.ItemID
			INNER JOIN dbo.InstitutionRole r WITH (NOLOCK) ON ii.InstitutionRoleID = r.InstitutionRoleID
	WHERE	(InstitutionCode = 'CANADIANA'
	OR		HasLocalContent = 0)
	AND		r.InstitutionRoleName = 'Holding Institution'
	AND		t.ItemID IS NOT NULL

	--############## SEGMENT ##############

	INSERT #Sortable (SegmentID, ItemSequence, Title, Authors, SearchAuthors, Subjects, Contributors, 
		Volume, PublisherName, PublicationPlace, HasSegments, HasLocalContent, HasExternalContent, 
		HasIllustrations, UniformTitle, SortTitle, PartNumber, PartName, LanguageName, Genre, DOIName, 
		Url, [Date], ContainerTitle, StartPageID, PageRange, StartPageNumber, EndPageNumber, Score)
	SELECT	scs.SegmentID,
			0 AS ItemSequence,
			s.Title,
			scs.Authors,
			scs.SearchAuthors,
			scs.Subjects,
			scs.Contributors,
			s.Volume,
			ISNULL(t.Datafield_260_b, ''),
			ISNULL(t.Datafield_260_a, ''),
			0 AS HasSegments,
			scs.HasLocalContent,
			scs.HasExternalContent,
			0 AS HasIllustrations,
			ISNULL(t.UniformTitle, ''),
			s.SortTitle,
			ISNULL(t.PartNumber, ''),
			ISNULL(t.PartName, ''),
			ISNULL(l.LanguageName, '') AS LanguageName,
			g.GenreName AS Genre,
            ISNULL(d.DOIName, '') AS DOIName,
			s.Url,
			s.[Date],
			ISNULL(t.FullTitle, s.ContainerTitle) AS ContainerTitle,
			s.StartPageID,
			s.PageRange,
			s.StartPageNumber,
			s.EndPageNumber,
			x.[RANK] AS Score
	FROM	CONTAINSTABLE(SearchCatalogSegment, (SearchText), @Search) x
			INNER JOIN dbo.SearchCatalogSegment scs WITH (NOLOCK) ON scs.SearchCatalogSegmentID = x.[KEY]
			INNER JOIN dbo.vwSegment s WITH (NOLOCK) ON scs.SegmentID = s.SegmentID
			INNER JOIN dbo.SegmentGenre g WITH (NOLOCK) ON s.SegmentGenreID = g.SegmentGenreID
			LEFT JOIN dbo.Language l WITH (NOLOCK) ON l.LanguageCode = s.LanguageCode
            LEFT JOIN dbo.DOI d WITH (NOLOCK) ON s.SegmentID = d.EntityID AND d.DOIStatusID IN (100, 200) AND d.DOIEntityTypeID = 40
			LEFT JOIN dbo.Item i WITH (NOLOCK) ON s.ItemID = i.ItemID
			LEFT JOIN dbo.Title t WITH (NOLOCK) ON i.PrimaryTitleID = t.TitleID

	-- De-emphasize ranking of any segments:
	--	1) Without local content
	UPDATE	#Sortable
	SET		Score = Score / 100.00
	WHERE	HasLocalContent = 0
	AND		SegmentID IS NOT NULL

	--############## FINISH UP ##############

	----------------------------------------------------------
	-- Return the sorted result set
	DECLARE @TotalHits int

	SELECT @TotalHits = COUNT(*) FROM #Sortable

	SELECT	@TotalHits AS TotalHits,
			TitleID,
			ItemID,
			SegmentID,
			Title,
			Authors,
			SearchAuthors,
			Subjects,
			Associations,
			Variants,
			Contributors,
			Volume,
			PublisherName,
			PublicationPlace,
			HasSegments,
			HasLocalContent,
			HasExternalContent,
			HasIllustrations,
			UniformTitle,
			SortTitle,
			PartNumber,
			PartName,
			LanguageName,
			Genre,
			MaterialTypeLabel,
			DOIName,
			Url,
			OCLC,
			ISSN,
			ISBN,
			Collections,
			ContainerTitle,
			StartPageID,
			PageRange,
			StartPageNumber,
			EndPageNumber,
			[Date],
			Score
	FROM	#Sortable
	ORDER BY
			Score DESC,
			SortTitle,
			ItemSequence
	OFFSET	@PageSize * (@StartPage - 1) ROWS
	FETCH NEXT @PageSize ROWS ONLY

END TRY
BEGIN CATCH
	DECLARE @ErrMsg varchar(350)
	SET @ErrMsg = 'Error searching publications for ' + @SearchText + ' ' + ERROR_MESSAGE()
	RAISERROR(@ErrMsg, 16, 1)
	RETURN 9 -- error occurred
END CATCH

END
