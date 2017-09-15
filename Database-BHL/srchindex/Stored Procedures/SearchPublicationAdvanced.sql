CREATE PROCEDURE srchIndex.SearchPublicationAdvanced

@Title			nvarchar(2000) = '',
@AuthorLastName	nvarchar(255) = '',
@Volume			nvarchar(100) = '',
@Year			smallint = NULL,
@Subject		nvarchar(50) = '',
@LanguageCode	nvarchar(10) = '',
@CollectionID	int = NULL,
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

	SET NOCOUNT ON

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


	DECLARE @SearchTitle nvarchar(4000)
	DECLARE @SearchAuthor nvarchar(4000)
	DECLARE @SearchVolume nvarchar(4000)
	DECLARE @SearchSubject nvarchar(4000)
	DECLARE @SearchYear nvarchar(4000)

	-- Transform the search terms into full-text search phrases
	SELECT @SearchTitle = dbo.fnGetFullTextSearchString(@Title)
	SELECT @SearchAuthor = dbo.fnGetFullTextSearchString(@AuthorLastName)
	SELECT @SearchVolume = dbo.fnGetFullTextSearchString(@Volume)
	SELECT @SearchSubject = dbo.fnGetFullTextSearchString(@Subject)
	SELECT @SearchYear = dbo.fnGetFullTextSearchString(@Year)


	--############## ITEM ##############

	-- Get item IDs by title string
	SELECT	t.TitleID, 
			i.ItemID,
			x.[RANK] AS TitleRank
	INTO	#TitleFilter
	FROM	CONTAINSTABLE(SearchCatalog, (FullTitle, Associations, Variants, UniformTitle), @SearchTitle) x
			INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON c.SearchCatalogID = x.[KEY]
			INNER JOIN dbo.Title t WITH (NOLOCK) ON c.TitleID = t.TitleID
			INNER JOIN dbo.Item i WITH (NOLOCK) ON t.TitleID = i.PrimaryTitleID AND i.ItemID = c.ItemID
	WHERE	t.PublishReady = 1
	AND		i.ItemStatusID = 40
	UNION
	SELECT	t.TitleID, i.ItemID, 0 
	FROM	dbo.Title t WITH (NOLOCK) 
			INNER JOIN dbo.Item i WITH (NOLOCK) ON t.TitleID = i.PrimaryTitleID
			INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON c.TitleID = t.TitleID AND c.ItemID = i.ItemID
	WHERE	@SearchTitle = '"**"' 
	AND		PublishReady = 1
	AND		ItemStatusID = 40

	-- Filter items by Language
	SELECT	i.TitleID,
			i.ItemID,
			i.TitleRank
	INTO	#LangFilter
	FROM	#TitleFilter i WITH (NOLOCK)
			INNER JOIN dbo.Item itm WITH (NOLOCK) on i.ItemID = itm.ItemID
			LEFT JOIN dbo.ItemLanguage il WITH (NOLOCK) ON i.ItemID = il.ItemID AND il.LanguageCode = @LanguageCode
	WHERE	itm.LanguageCode = @LanguageCode 
	OR		ISNULL(il.LanguageCode, '') = @LanguageCode 
	OR		@LanguageCode = ''

	-- Filter items by collection
	SELECT	ItemID
	INTO	#CollectionItems
	FROM	dbo.ItemCollection WITH (NOLOCK)
	WHERE	CollectionID = @CollectionID
	UNION
	SELECT	ItemID
	FROM	dbo.TitleCollection tc WITH (NOLOCK)
			INNER JOIN dbo.Title t WITH (NOLOCK) ON tc.TitleID = t.TitleID
			INNER JOIN dbo.Item i WITH (NOLOCK) ON t.TitleID = i.PrimaryTitleID
	WHERE	CollectionID = @CollectionID


	CREATE TABLE #CollectionFilter
		(
		TitleID INT NOT NULL,
		ItemID INT NOT NULL,
		TitleRank INT NOT NULL
		)

	IF EXISTS(SELECT ItemID FROM #CollectionItems)
	BEGIN
		INSERT	#CollectionFilter
		SELECT	i.TitleID,
				i.ItemID,
				i.TitleRank
		FROM	#LangFilter i 
				INNER JOIN #CollectionItems ci ON i.ItemID = ci.ItemID
	END
	ELSE
	BEGIN
		INSERT	#CollectionFilter
		SELECT	TitleID, ItemID, TitleRank
		FROM	#LangFilter
	END

	-- Filter items by year
	SELECT	tmp.TitleID,
			tmp.ItemID,
			tmp.TitleRank			
	INTO	#YearFilter
	FROM	#CollectionFilter tmp 
			INNER JOIN dbo.Title t WITH (NOLOCK) ON tmp.TitleID = t.TitleID
			INNER JOIN dbo.Item i WITH (NOLOCK) ON tmp.ItemID = i.ItemID
	WHERE	@Year IS NULL
	OR 		ISNULL(i.[Year], '') = CONVERT(nvarchar(20), @Year) 
	OR		ISNULL(t.StartYear, -1) = @Year 
	OR 		ISNULL(t.EndYear, -1) = @Year 
	OR 		(@Year BETWEEN t.StartYear AND t.EndYear)

	-- Filter by author
	SELECT	tmp.TitleID,
			tmp.ItemID,
			tmp.TitleRank,
			x.[RANK] AS AuthorRank
	INTO	#AuthorFilter
	FROM	CONTAINSTABLE(SearchCatalog, SearchAuthors, @SearchAuthor) x
			INNER JOIN SearchCatalog c WITH (NOLOCK) ON c.SearchCatalogID = x.[KEY]
			INNER JOIN #YearFilter tmp ON c.TitleID = tmp.TitleID AND c.ItemID = tmp.ItemID
	UNION
	SELECT DISTINCT TitleID, ItemID, TitleRank, 0 AS AuthorRank FROM #YearFilter WHERE @SearchAuthor = '"**"'

	-- Filter item by subject
	SELECT	tmp.TitleID,
			tmp.ItemID,
			tmp.TitleRank,
			tmp.AuthorRank,
			x.[RANK] AS SubjectRank
	INTO	#SubjectFilter
	FROM	CONTAINSTABLE(SearchCatalog, Subjects, @SearchSubject) x
			INNER JOIN SearchCatalog c WITH (NOLOCK) ON c.SearchCatalogID = x.[KEY]
			INNER JOIN #AuthorFilter tmp ON c.TitleID = tmp.TitleID AND c.ItemID = tmp.ItemID
	UNION
	SELECT DISTINCT TitleID, ItemID, TitleRank, AuthorRank, 0 AS SubjectRank FROM #AuthorFilter WHERE @SearchSubject = '"**"'

	-- Filter items by volume
	CREATE TABLE #VolumeFilter
		(
		TitleID INT NOT NULL,
		ItemID INT NOT NULL,
		TitleRank INT NOT NULL,
		AuthorRank INT NOT NULL,
		SubjectRank INT NOT NULL
		)

	IF EXISTS (
		SELECT	tmp.ItemID
		FROM	#SubjectFilter tmp INNER JOIN dbo.Item i WITH (NOLOCK) ON tmp.ItemID = i.ItemID
		WHERE	i.StartVolume = @SearchVolume
		OR		i.EndVolume = @SearchVolume
		OR		@SearchVolume BETWEEN i.StartVolume AND i.EndVolume
		)
	BEGIN
		-- Found at least one volume match, so only return volume matches
		INSERT	#VolumeFilter
		SELECT	tmp.TitleID,
				tmp.ItemID,
				tmp.TitleRank,
				tmp.AuthorRank,
				tmp.SubjectRank
		FROM	#SubjectFilter tmp INNER JOIN dbo.Item i WITH (NOLOCK) ON tmp.ItemID = i.ItemID
		WHERE	i.StartVolume = @SearchVolume
		OR		i.EndVolume = @SearchVolume
		OR		@SearchVolume BETWEEN i.StartVolume AND i.EndVolume
	END
	ELSE
	BEGIN
		-- No matches, so return everything we had accumulated to this point.  We
		-- want users to know that we have a title, even if we don't have the exact
		-- volume that they asked for.  This also helps if the volume data is messy 
		-- (maybe we DO have the volume, but the initial search didn't find it).
		INSERT	#VolumeFilter 
		SELECT	TitleID, ItemID, TitleRank, AuthorRank, SubjectRank 
		FROM	#SubjectFilter
	END

	----------------------------------------------------------
	-- Compile the final sortable result set.
	INSERT #Sortable (TitleID, ItemID, ItemSequence, Title, Authors, SearchAuthors, Subjects, Associations,
		Variants, Contributors, Volume, PublisherName, PublicationPlace, HasSegments, HasLocalContent,
		HasExternalContent, HasIllustrations, UniformTitle, SortTitle, PartNumber, PartName, LanguageName,
		Genre, MaterialTypeLabel, DOIName, Url, OCLC, ISSN, ISBN, Collections, [Date], Score)
	SELECT	tmp.TitleID,
			i.ItemID,
			ti.ItemSequence,
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
			(20 * TitleRank) + (10 * AuthorRank) + (5 * SubjectRank) AS Score
	FROM	#VolumeFilter tmp 
			INNER JOIN dbo.Title t WITH (NOLOCK) ON tmp.TitleID = t.TitleID
			INNER JOIN dbo.Item i WITH (NOLOCK) ON tmp.ItemID = i.ItemID
			INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON t.TitleID = ti.TitleID AND ti.ItemID = tmp.ItemID
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

	-- Don't bother looking for segments if a collection was specified (no segment collections exist)
	IF NOT EXISTS(SELECT ItemID FROM #CollectionItems)
	BEGIN

		CREATE TABLE #tmpInitialResult
			(
			SegmentID int NOT NULL,
			TitleRank int NULL,
			AuthorRank int NULL
			)

		-- Get initial result set, filtering by Title and/or Author
		IF (@SearchTitle <> '"**"' AND @SearchAuthor <> '"**"')
		BEGIN
			INSERT #tmpInitialResult (SegmentID, TitleRank, AuthorRank)
			SELECT	c.SegmentID,
					x.RANK,
					y.RANK
			FROM	CONTAINSTABLE(SearchCatalogSegment, (Title), @SearchTitle) x
					INNER JOIN CONTAINSTABLE(SearchCatalogSegment, (SearchAuthors), @SearchAuthor) y ON x.[KEY] = y.[KEY]
					INNER JOIN dbo.SearchCatalogSegment c ON c.SearchCatalogSegmentID = x.[KEY]
					INNER JOIN dbo.Segment s ON c.SegmentID = s.SegmentID
			WHERE	s.SegmentStatusID IN (10, 20) -- New, Published
		END

		IF (@SearchTitle <> '"**"' AND @SearchAuthor = '"**"')
		BEGIN
			INSERT #tmpInitialResult (SegmentID, TitleRank)
			SELECT	c.SegmentID,
					x.RANK
			FROM	CONTAINSTABLE(SearchCatalogSegment, (Title), @SearchTitle) x
					INNER JOIN dbo.SearchCatalogSegment c ON c.SearchCatalogSegmentID = x.[KEY]
					INNER JOIN dbo.Segment s ON c.SegmentID = s.SegmentID
			WHERE	s.SegmentStatusID IN (10, 20) -- New, Published
		END

		IF (@SearchTitle = '"**"' AND @SearchAuthor <> '"**"')
		BEGIN
			INSERT #tmpInitialResult (SegmentID, AuthorRank)
			SELECT	c.SegmentID,
					x.RANK
			FROM	CONTAINSTABLE(SearchCatalogSegment, (SearchAuthors), @SearchAuthor) x
					INNER JOIN dbo.SearchCatalogSegment c ON c.SearchCatalogSegmentID = x.[KEY]
					INNER JOIN dbo.Segment s ON c.SegmentID = s.SegmentID
			WHERE	s.SegmentStatusID IN (10, 20) -- New, Published
		END

		-- Limit results by date
		SELECT	t.SegmentID, t.TitleRank, t.AuthorRank, x.[RANK] AS DateRank
		INTO	#tmpLimitYear
		FROM	CONTAINSTABLE(SearchCatalogSegment, ([Date]), @SearchYear) x
				INNER JOIN dbo.SearchCatalogSegment c ON c.SearchCatalogSegmentID = x.[KEY]
				INNER JOIN #tmpInitialResult t ON c.SegmentID = t.SegmentID
		UNION
		SELECT	SegmentID, TitleRank, AuthorRank, 0 AS DateRank 
		FROM	#tmpInitialResult WHERE @SearchYear = '"**"'

		-- Limit results by volume
		SELECT	t.SegmentID, t.TitleRank, t.AuthorRank, t.DateRank, x.[RANK] AS VolumeRank
		INTO	#tmpLimitVolume
		FROM	CONTAINSTABLE(SearchCatalogSegment, (Volume), @SearchVolume) x
				INNER JOIN dbo.SearchCatalogSegment c ON c.SearchCatalogSegmentID = x.[KEY]
				INNER JOIN #tmpLimitYear t ON c.SegmentID = t.SegmentID
		UNION
		SELECT	SegmentID, TitleRank, AuthorRank, DateRank, 0 AS VolumeRank 
		FROM	#tmpLimitYear WHERE @SearchVolume = '"**"'

		-- Filter item by subject
		SELECT	t.SegmentID, t.TitleRank, t.AuthorRank, t.DateRank, t.VolumeRank, x.[RANK] AS SubjectRank
		INTO	#tmpLimitSubject
		FROM	CONTAINSTABLE(SearchCatalogSegment, (Subjects), @SearchSubject) x
				INNER JOIN dbo.SearchCatalogSegment c ON c.SearchCatalogSegmentID = x.[KEY]
				INNER JOIN #tmpLimitVolume t ON c.SegmentID = t.SegmentID
		UNION
		SELECT	SegmentID, TitleRank, AuthorRank, DateRank, VolumeRank, 0 AS SubjectRank
		FROM	#tmpLimitVolume WHERE @SearchSubject = '"**"'

		-- Limit results by language
		SELECT	t.SegmentID, TitleRank, AuthorRank, DateRank, VolumeRank, SubjectRank
		INTO	#tmpLimitFinal
		FROM	#tmpLimitSubject t
				INNER JOIN dbo.Segment s WITH (NOLOCK) ON t.SegmentID = s.SegmentID
		WHERE	s.LanguageCode = @LanguageCode
		OR		@LanguageCode = ''

		-- Get the rest of the segment details
		INSERT #Sortable (SegmentID, ItemSequence, Title, Authors, SearchAuthors, Subjects, Contributors, 
			Volume, PublisherName, PublicationPlace, HasSegments, HasLocalContent, HasExternalContent, 
			HasIllustrations, UniformTitle, SortTitle, PartNumber, PartName, LanguageName, Genre, DOIName, 
			Url, [Date], ContainerTitle, StartPageID, PageRange, StartPageNumber, EndPageNumber, Score)
		SELECT	s.SegmentID,
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
				ISNULL(tmp.TitleRank, 0) + ISNULL(tmp.AuthorRank, 0) + ISNULL(tmp.DateRank, 0) + ISNULL(tmp.VolumeRank, 0) AS Score
		FROM	#tmpLimitFinal tmp INNER JOIN dbo.vwSegment s ON tmp.SegmentID = s.SegmentID
				INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
				INNER JOIN dbo.SearchCatalogSegment scs ON s.SegmentID = scs.SegmentID
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

	END

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
	SET @ErrMsg = 'Error searching publications for ' + @Title + '|' + @AuthorLastName + '|' + 
					@Volume + '|' + ISNULL(CONVERT(varchar(max), @Year), '') + '|' + @Subject + '|' + @LanguageCode + '|' + 
					ISNULL(CONVERT(varchar(20), @CollectionID), '') + ERROR_MESSAGE()
	RAISERROR(@ErrMsg, 16, 1)
	RETURN 9 -- error occurred
END CATCH

END
