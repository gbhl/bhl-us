CREATE PROCEDURE [dbo].[SearchCatalogRefresh]

@Target nvarchar(20) = 'Books'

AS

BEGIN

/*--------------------------------------------------------------------------------------------
--
--	This procedure should be executed after every major modification to searchable metadata.
--	It updates the data held in the search tables and full-text indexes.
--
--	Note that if an error occurs while the data in the catalog tables is being updated, the
--  search catalog is left offline, which means that full-text search will NOT be used.
--
--  DDL For Full-Text Indexes:
--
		CREATE FULLTEXT CATALOG BHLSearchCatalog IN PATH 'D:\Databases' WITH ACCENT_SENSITIVITY = OFF
		GO

		CREATE FULLTEXT INDEX ON dbo.SearchCatalog
		(
			  SearchText,
			  FullTitle,
			  UniformTitle,
			  Volume,
			  PublicationDetails,
			  PublisherPlace,
			  PublisherName,
			  EditionStatement,
			  Subjects,
			  Associations,
			  Variants,
			  SearchAuthors
		)
		KEY INDEX PK_SearchCatalog ON BHLSearchCatalog
		GO

		CREATE FULLTEXT INDEX ON dbo.SearchCatalogKeyword
		(
			  Keyword
		)
		KEY INDEX PK_SearchCatalogKeyword ON BHLSearchCatalog
		GO

		CREATE FULLTEXT INDEX ON dbo.SearchCatalogCreator
		(
			  CreatorName
		)
		KEY INDEX PK_SearchCatalogCreator ON BHLSearchCatalog
		GO
		
		CREATE FULLTEXT INDEX ON dbo.SearchCatalogSegment
		(
			SearchText,
			Title,
			TranslatedTitle,
			ContainerTitle,
			PublicationDetails,
			Volume,
			Series,
			Issue,
			Date,
			Subjects,
			SearchAuthors
		)
		KEY INDEX PK_SearchCatalogSegment ON BHLSearchCatalog
		GO
		
--
---------------------------------------------------------------------------------------------*/

SET NOCOUNT ON

-- ********************************************************************************************
-- *************                             GATHER DATA                          *************
-- ********************************************************************************************

-- ************************************  SEARCHCATALOGTITLETAG  *******************************

-- Create a temporary table with all of the title tag data
CREATE TABLE #tmpSearchCatalogKeyword
(
	SearchCatalogKeywordID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	KeywordID int NOT NULL,
	Keyword nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AI NOT NULL DEFAULT('')
)

-- Populate the temporary table
INSERT	#tmpSearchCatalogKeyword (KeywordID, Keyword)
SELECT DISTINCT KeywordID, Keyword
FROM	dbo.Keyword
WHERE	LTRIM(RTRIM(Keyword)) <> ''


-- ************************************  SEARCHCATALOGCREATOR  ********************************

-- Create a temporary table with all of the creator data
CREATE TABLE #tmpSearchCatalogCreator
(
	SearchCatalogCreatorID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	CreatorID int NOT NULL,
	CreatorName nvarchar(2000) NOT NULL DEFAULT('')
)

-- Populate the temporary table
INSERT	#tmpSearchCatalogCreator(CreatorID, CreatorName)
SELECT DISTINCT
		a.AuthorID,
		dbo.fnAuthorSearchStringForAuthor(a.AuthorID, ' ')
FROM	dbo.AuthorName n INNER JOIN dbo.Author a ON n.AuthorID = a.AuthorID
WHERE	LTRIM(RTRIM(n.FullName)) <> ''
AND		a.IsActive = 1

-- ****************************************  SEARCHCATALOG  ***********************************

IF (@Target = 'Books' OR @Target = '')
BEGIN

	-- Create a temporary table with all of the searchable title and item data
	CREATE TABLE #tmpSearchCatalog
	(
		SearchCatalogID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
		TitleID int NOT NULL,
		ItemID int NOT NULL,
		FirstPageID int NULL,
		SearchText nvarchar(4000) NOT NULL DEFAULT(''),
		FullTitle nvarchar(2000) NOT NULL DEFAULT(''),
		UniformTitle nvarchar(255) NOT NULL DEFAULT(''),
		PublicationDetails nvarchar(255) NULL DEFAULT(''),
		PublisherPlace nvarchar(150) NULL DEFAULT(''),
		PublisherName nvarchar(255) NULL DEFAULT(''),
		Volume nvarchar(100) NOT NULL DEFAULT(''),
		EditionStatement nvarchar(450) NOT NULL DEFAULT(''),
		Subjects nvarchar(max) NOT NULL DEFAULT(''),
		Associations nvarchar(max) NOT NULL DEFAULT(''),
		Variants nvarchar(max) NOT NULL DEFAULT(''),
		Authors nvarchar(max) NOT NULL DEFAULT(''),
		SearchAuthors nvarchar(max) NOT NULL DEFAULT(''),
		TitleContributors nvarchar(max) NOT NULL DEFAULT(''),
		ItemContributors nvarchar(max) NOT NULL DEFAULT(''),
		HasSegments smallint NOT NULL DEFAULT(0),
		HasLocalContent smallint NOT NULL DEFAULT(1),
		HasExternalContent smallint NOT NULL DEFAULT(0),
		HasIllustrations smallint NOT NULL DEFAULT(0)
	)

	/*
	-- Populate the temporary table
	INSERT  #tmpSearchCatalog (TitleID, ItemID, SearchText, FullTitle, UniformTitle,
					PublicationDetails, PublisherPlace, PublisherName, Volume, 
					EditionStatement, Subjects, Associations, Variants, Authors)
	SELECT	t.TitleID,
			i.ItemID,
			ISNULL(t.FullTitle + ' ', '') +
				ISNULL(t.PartNumber + ' ', '') +
				ISNULL(t.PartName + ' ', '') +
				ISNULL(t.UniformTitle COLLATE SQL_Latin1_General_CP1_CI_AI + ' ', '') +
				ISNULL(i.Volume + ' ', '') +
				ISNULL(t.EditionStatement COLLATE SQL_Latin1_General_CP1_CI_AI + ' ', '') +
				CASE WHEN LEN(i.Year) > 0 THEN i.Year + ' '
					WHEN LEN(t.StartYear) > 0 OR LEN(t.EndYear) > 0
					THEN LTRIM(RTRIM(ISNULL(CONVERT(nvarchar(4), t.StartYear), '') + ' ' + ISNULL(CONVERT(nvarchar(4), t.EndYear), ''))) + ' '
					ELSE ''
					END +
				ISNULL(REPLACE(dbo.fnKeywordStringForTitle(t.TitleID), '|', ' ') + ' ', '') +
				REPLACE(dbo.fnAssociationStringForTitle(t.TitleID), '|', ' ') + ' ' + 
				ISNULL(REPLACE(dbo.fnCOinSAuthorStringForTitle(t.TitleID, 0), '|', ' ') + ' ', '') +
				ISNULL(REPLACE(dbo.fnVariantStringForTitle(t.TitleID), '|', ' ') + ' ', '') AS SearchText,
			ISNULL(t.FullTitle + ' ', '') +
				ISNULL(t.PartNumber + ' ', '') +
				ISNULL(t.PartName + ' ', '') AS FullTitle,
			ISNULL(t.UniformTitle, ''),
			ISNULL(t.PublicationDetails, ''),
			ISNULL(t.Datafield_260_a, ''),
			ISNULL(t.Datafield_260_b, ''),
			ISNULL(i.Volume, ''),
			ISNULL(t.EditionStatement, ''),
			ISNULL(REPLACE(dbo.fnKeywordStringForTitle(t.TitleID), '|', ' '), ''),
			REPLACE(dbo.fnAssociationStringForTitle(t.TitleID), '|', ' '),
			ISNULL(REPLACE(dbo.fnVariantStringForTitle(t.TitleID), '|', ' '), ''),
			ISNULL(REPLACE(dbo.fnCOinSAuthorStringForTitle(t.TitleID, 0), '|', ' '), '')
	FROM	dbo.Title t WITH (NOLOCK)
			INNER JOIN dbo.TitleItem ti WITH (NOLOCK) ON t.TitleID = ti.TitleID
			INNER JOIN dbo.Item i WITH (NOLOCK) ON ti.ItemID = i.ItemID
	WHERE	t.PublishReady = 1
	AND		i.ItemStatusID = 40
	*/

	CREATE TABLE #tmpItem
		(
		SearchCatalogID int IDENTITY(1,1) NOT NULL , --PRIMARY KEY,
		TitleID int NOT NULL,
		ItemID int NOT NULL,
		FirstPageID int NULL,
		SearchText nvarchar(4000) NOT NULL DEFAULT(''),
		FullTitle nvarchar(2000) NOT NULL DEFAULT(''),
		PartNumber nvarchar(255) NULL DEFAULT(''),
		PartName nvarchar(255) NULL DEFAULT(''),
		UniformTitle nvarchar(255) NOT NULL DEFAULT(''),
		PublicationDetails nvarchar(255) NULL DEFAULT(''),
		PublisherPlace nvarchar(150) NULL DEFAULT(''),
		PublisherName nvarchar(255) NULL DEFAULT(''),
		Volume nvarchar(100) NOT NULL DEFAULT(''),
		EditionStatement nvarchar(450) NOT NULL DEFAULT(''),
		[Year] nvarchar(20) NOT NULL DEFAULT(''),
		Subjects nvarchar(max) NOT NULL DEFAULT(''),
		Associations nvarchar(max) NOT NULL DEFAULT(''),
		Variants nvarchar(max) NOT NULL DEFAULT(''),
		Authors nvarchar(max) NOT NULL DEFAULT(''),
		SearchAuthors nvarchar(max) NOT NULL DEFAULT(''),
		TitleContributors nvarchar(max) NOT NULL DEFAULT(''),
		ItemContributors nvarchar(max) NOT NULL DEFAULT(''),
  		HasSegments smallint NOT NULL DEFAULT(0),
		HasLocalContent smallint NOT NULL DEFAULT(0),
		HasExternalContent smallint NOT NULL DEFAULT(0),
		HasIllustrations smallint NOT NULL DEFAULT(0)
		)

	-- Get the initial data set
	INSERT	#tmpItem (TitleID, ItemID, FullTitle, PartNumber, PartName, UniformTitle, 
			Volume, PublicationDetails, PublisherPlace, PublisherName, EditionStatement, [Year],
			HasSegments, HasExternalContent)
	SELECT DISTINCT
			t.TitleID,
			i.ItemID,
			ISNULL(RTRIM(t.FullTitle) + ' ', '') AS FullTitle,
			ISNULL(RTRIM(t.PartNumber) + ' ', '') AS PartNumber,
			ISNULL(RTRIM(t.PartName) + ' ', '') AS PartName,
			ISNULL(RTRIM(t.UniformTitle) COLLATE SQL_Latin1_General_CP1_CI_AI + ' ', '') AS UniformTitle,
			ISNULL(RTRIM(i.Volume) + ' ', '') AS Volume,
			ISNULL(RTRIM(t.PublicationDetails) + ' ', '') AS PublicationDetails,
			ISNULL(RTRIM(t.Datafield_260_a) + ' ', '') AS PublisherPlace,
			ISNULL(RTRIM(t.Datafield_260_b) + ' ', '') AS PublisherName,
			ISNULL(RTRIM(t.EditionStatement) COLLATE SQL_Latin1_General_CP1_CI_AI + ' ', '') AS EditionStatement,
			CASE WHEN LEN(i.Year) > 0 THEN i.Year + ' '
				WHEN LEN(t.StartYear) > 0 OR LEN(t.EndYear) > 0
				THEN LTRIM(RTRIM(ISNULL(CONVERT(nvarchar(4), t.StartYear), '') + ' ' + ISNULL(CONVERT(nvarchar(4), t.EndYear), ''))) + ' '
				ELSE ''
				END AS [Year],
			CASE WHEN s.SegmentID IS NULL THEN 0 ELSE 1 END,
			CASE WHEN ISNULL(RTRIM(i.ExternalUrl), '') = '' THEN 0 ELSE 1 END
	FROM	dbo.Title t 
			INNER JOIN dbo.TitleItem ti ON t.TitleID = ti.TitleID
			INNER JOIN dbo.Item i ON ti.ItemID = i.ItemID
			LEFT JOIN dbo.Segment s ON i.ItemID = s.ItemID AND s.SegmentStatusID IN (10, 20)
	WHERE	t.PublishReady = 1
	AND		i.ItemStatusID = 40

	-- Add subjects, associations, authors, variants, and contributors
	UPDATE	#tmpItem
	SET		Subjects = ISNULL(RTRIM(dbo.fnKeywordStringForTitle(t.TitleID)) + ' ', '')
	FROM	#tmpItem t INNER JOIN (SELECT DISTINCT TitleID FROM dbo.TitleKeyword) tt ON t.TitleID = tt.TitleID

	UPDATE	#tmpItem
	SET		Associations = ISNULL(RTRIM(dbo.fnAssociationStringForTitle(t.TitleID)) + ' ', '')
	FROM	#tmpItem t INNER JOIN (SELECT DISTINCT TitleID FROM dbo.TitleAssociation) a ON t.TitleID = a.TitleID

	UPDATE	#tmpItem
	SET		Authors = ISNULL(RTRIM(dbo.fnAuthorSearchStringForTitle(TitleID, 1)) + ' ', '')

	UPDATE	#tmpItem
	SET		SearchAuthors = ISNULL(RTRIM(dbo.fnAuthorSearchStringForTitle(TitleID, 0)) + ' ', '')

	UPDATE	#tmpItem
	SET		Variants = ISNULL(RTRIM(dbo.fnVariantStringForTitle(t.TitleID)) + ' ', '')
	FROM	#tmpItem t INNER JOIN (SELECT DISTINCT TitleID FROM dbo.TitleVariant) v ON t.TitleID = v.TitleID

	UPDATE	#tmpItem
	SET		TitleContributors = ISNULL(RTRIM(dbo.fnContributorStringForTitle(TitleID, 1)) + ' ', '')

	UPDATE	#tmpItem
	SET		ItemContributors = ISNULL(RTRIM(dbo.fnContributorStringForItem(ItemID)) + ' ', '')

	UPDATE	#tmpItem
	SET		HasLocalContent = 1
	FROM	#tmpItem t INNER JOIN dbo.Page p ON t.ItemID = p.ItemID

	UPDATE	#tmpItem
	SET		HasIllustrations = x.HasIllustrations
	FROM	#tmpItem i INNER JOIN (
				SELECT	t.ItemID, MAX(CASE WHEN ppt.PageID IS NULL THEN 0 ELSE 1 END) AS HasIllustrations
				FROM	#tmpItem t
						INNER JOIN dbo.Page p ON t.ItemID = p.ItemID
						-- 3 = Illustration
						-- 10 = Map
						-- 14 = Foldout
						-- 18 = Drawing
						-- 19 = Table
						-- 20 = Photograph
						LEFT JOIN dbo.Page_PageType ppt ON p.PageID = ppt.PageID AND ppt.PageTypeID IN (3, 10, 14, 18, 19, 20)
				GROUP BY
						t.ItemID
				) x ON i.ItemID = x.ItemID

	-- Get the first page IDs for each item
	CREATE TABLE #tmpPages (SequenceOrder int NULL, ITEMID int NOT NULL)

	INSERT INTO #tmpPages
	SELECT	MIN(p.SequenceOrder) AS SequenceOrder, 
			i.ItemID
	FROM	#tmpItem i 
			INNER JOIN dbo.Page p WITH (NOLOCK) ON i.ItemID = p.ItemID
			INNER JOIN Page_PageType ppt ON p.PageID = ppt.PageID
	WHERE	p.Active = 1
	AND		ppt.PageTypeID = 1 -- Use the first Title Page in the book
	GROUP BY i.ItemID

	UPDATE	#tmpItem
	SET		FirstPageID = p.PageID
	FROM	#tmpItem i
			INNER JOIN #tmpPages t ON i.ItemID = t.ItemID
			INNER JOIN dbo.Page p WITH (NOLOCK) ON t.ItemID = p.ItemID AND t.SequenceOrder = p.SequenceOrder

	UPDATE	#tmpItem
	SET		FirstPageID = p.PageID	-- Just use the first physical page in the book if no title page found
	FROM	#tmpItem i
			INNER JOIN dbo.Page p WITH (NOLOCK) ON p.ItemID = i.ItemID AND p.SequenceOrder = 1
	WHERE	i.FirstPageID IS NULL

	DROP TABLE #tmpPages

	-- Populate the temp table we'll use to update the search catalog
	INSERT  #tmpSearchCatalog (TitleID, ItemID, FirstPageID, SearchText, FullTitle, 
				UniformTitle, PublicationDetails, PublisherPlace, PublisherName,
				Volume, EditionStatement, Subjects, Associations, Variants, Authors,
				SearchAuthors, TitleContributors, ItemContributors, HasSegments, 
				HasLocalContent, HasExternalContent, HasIllustrations)
	SELECT	TitleID,
			ItemID,
			FirstPageID,
			LEFT(FullTitle +
				PartNumber +
				PartName +
				UniformTitle +
				Volume +
				EditionStatement +
				[Year] +
				Subjects +
				Associations +
				SearchAuthors +
				Variants, 4000) AS SearchText,
			LEFT(RTRIM(FullTitle + PartNumber + PartName), 2000),
			RTRIM(UniformTitle),
			RTRIM(PublicationDetails),
			RTRIM(PublisherPlace),
			RTRIM(PublisherName),
			RTRIM(Volume),
			RTRIM(EditionStatement),
			RTRIM(Subjects),
			RTRIM(Associations),
			RTRIM(Variants),
			RTRIM(Authors),
			RTRIM(SearchAuthors),
			RTRIM(TitleContributors),
			RTRIM(ItemContributors),
			HasSegments,
			HasLocalContent,
			HasExternalContent,
			HasIllustrations
	FROM	#tmpItem

	DROP TABLE #tmpItem
END

-- *************************************  SEARCHCATALOGSEGMENT *******************************

IF (@Target = 'Segments' OR @Target = '')
BEGIN

	-- Create a temporary table with all of the searchable title and item data
	CREATE TABLE #tmpSegment
	(
		SearchCatalogSegmentID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
		SegmentID int NOT NULL,
		ItemID int NULL,
		Title nvarchar(2000) NOT NULL DEFAULT(''),
		TranslatedTitle nvarchar(2000) NOT NULL DEFAULT(''),
		ContainerTitle nvarchar(2000) NOT NULL DEFAULT(''),
		PublicationDetails nvarchar(400) NULL DEFAULT(''),
		Volume nvarchar(100) NOT NULL DEFAULT(''),
		Series nvarchar(100) NOT NULL DEFAULT(''),
		Issue nvarchar(100) NOT NULL DEFAULT(''),
		[Date] nvarchar(20) NOT NULL DEFAULT(''),
		Subjects nvarchar(max) NOT NULL DEFAULT(''),
		Authors nvarchar(max) NOT NULL DEFAULT(''),
		SearchAuthors nvarchar(max) NOT NULL DEFAULT(''),
		Contributors nvarchar(max) NOT NULL DEFAULT(''),
		HasLocalContent smallint NOT NULL DEFAULT(1),
		HasExternalContent smallint NOT NULL DEFAULT(0),
		HasIllustrations smallint NOT NULL DEFAULT(0)
	)

	-- Get the initial data set
	INSERT  #tmpSegment (SegmentID, ItemID, Title, TranslatedTitle,
					ContainerTitle, PublicationDetails, Volume, Series, Issue, 
					[Date], Subjects, Authors, HasLocalContent, HasExternalContent)
	SELECT DISTINCT
			s.SegmentID,
			s.ItemID,
			RTRIM(s.Title) + ' ',
			RTRIM(s.TranslatedTitle) + ' ',
			RTRIM(s.ContainerTitle) + ' ' AS ContainerTitle,
			RTRIM(CASE WHEN (s.PublisherPlace = '' AND s.PublisherName = '')
				THEN ISNULL(s.PublicationDetails, '')
				ELSE s.PublisherPlace + CASE WHEN s.PublisherPlace <> '' THEN ': ' ELSE '' END + s.PublisherName
			END) + ' ' AS PublicationDetails,
			RTRIM(s.Volume) + ' ',
			RTRIM(s.Series) + ' ',
			RTRIM(s.Issue) + ' ',
			RTRIM(s.[Date]) + ' ',
			'',
			'',
			CASE WHEN p.SegmentPageID IS NULL THEN 0 ELSE 1 END,
			RTRIM(CASE WHEN s.Url = '' THEN 0 ELSE 1 END)
	FROM	dbo.vwSegment s LEFT JOIN dbo.Item i ON s.ItemID = i.ItemID
			LEFT JOIN dbo.Title t ON i.PrimaryTitleID = t.TitleID
			LEFT JOIN dbo.SegmentPage p ON s.SegmentID = p.SegmentID
	WHERE	s.SegmentStatusID IN (10, 20)

	-- Add subjects, authors, and conributors
	UPDATE	#tmpSegment
	SET		Subjects = ISNULL(RTRIM(dbo.fnKeywordStringForSegment(SegmentID)) + ' ', '')

	UPDATE	#tmpSegment
	SET		Authors = ISNULL(RTRIM(dbo.fnAuthorSearchStringForSegment(SegmentID, 1)) + ' ', '')

	UPDATE	#tmpSegment
	SET		SearchAuthors = ISNULL(RTRIM(dbo.fnAuthorSearchStringForSegment(SegmentID, 0)) + ' ', '')

	UPDATE	#tmpSegment
	SET		Contributors = ISNULL(RTRIM(dbo.fnContributorStringForSegment(SegmentID)) + ' ', '')
	
	UPDATE	#tmpSegment
	SET		HasIllustrations = x.HasIllustrations
	FROM	#tmpSegment s INNER JOIN (
				SELECT	t.SegmentID, MAX(CASE WHEN ppt.PageID IS NULL THEN 0 ELSE 1 END) AS HasIllustrations
				FROM	#tmpSegment t
						INNER JOIN dbo.SegmentPage p ON t.SegmentID = p.SegmentID
						-- 3 = Illustration
						-- 10 = Map
						-- 14 = Foldout
						-- 18 = Drawing
						-- 19 = Table
						-- 20 = Photograph
						LEFT JOIN dbo.Page_PageType ppt ON p.PageID = ppt.PageID AND ppt.PageTypeID IN (3, 10, 14, 18, 19, 20)
				GROUP BY
						t.SegmentID
				) x ON s.SegmentID = x.SegmentID

	-- Create and populate the temp table we'll use to update the search catalog
	CREATE TABLE #tmpSearchCatalogSegment
	(
		SearchCatalogSegmentID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
		SegmentID int NOT NULL,
		ItemID int NULL,
		SearchText nvarchar(4000) NOT NULL DEFAULT(''),
		Title nvarchar(2000) NOT NULL DEFAULT(''),
		TranslatedTitle nvarchar(2000) NOT NULL DEFAULT(''),
		ContainerTitle nvarchar(2000) NOT NULL DEFAULT(''),
		PublicationDetails nvarchar(400) NULL DEFAULT(''),
		Volume nvarchar(100) NOT NULL DEFAULT(''),
		Series nvarchar(100) NOT NULL DEFAULT(''),
		Issue nvarchar(100) NOT NULL DEFAULT(''),
		[Date] nvarchar(20) NOT NULL DEFAULT(''),
		Subjects nvarchar(max) NOT NULL DEFAULT(''),
		Authors nvarchar(max) NOT NULL DEFAULT(''),
		SearchAuthors nvarchar(max) NOT NULL DEFAULT(''),
		Contributors nvarchar(max) NOT NULL DEFAULT(''),
		HasLocalContent smallint NOT NULL DEFAULT(1),
		HasExternalContent smallint NOT NULL DEFAULT(0),
		HasIllustrations smallint NOT NULL DEFAULT(0)
	)
	
	INSERT  #tmpSearchCatalogSegment (SegmentID, ItemID, SearchText, Title, TranslatedTitle,
				ContainerTitle, PublicationDetails, Volume, Series, Issue, [Date],
				Subjects, Authors, SearchAuthors, Contributors, HasLocalContent, HasExternalContent,
				HasIllustrations)
	SELECT	SegmentID,
			ItemID,
			LEFT(Title +
				ContainerTitle +
				Volume +
				Series +
				Issue +
				[Date] +
				Subjects +
				SearchAuthors, 4000) AS SearchText,
			RTRIM(Title),
			RTRIM(TranslatedTitle),
			RTRIM(ContainerTitle),
			RTRIM(PublicationDetails),
			RTRIM(Volume),
			RTRIM(Series),
			RTRIM(Issue),
			RTRIM([Date]),
			RTRIM(Subjects),
			RTRIM(Authors),
			RTRIM(SearchAuthors),
			RTRIM(Contributors),
			HasLocalContent,
			HasExternalContent,
			HasIllustrations
	FROM	#tmpSegment

	DROP TABLE #tmpSegment

END

-- ********************************************************************************************
-- *************                          UPDATE CATALOGS                         *************
-- ********************************************************************************************

-- Mark the searchcatalog (full-text search) as offline while we update the data
UPDATE dbo.Configuration SET ConfigurationValue = 1 WHERE ConfigurationName = 'SearchCatalogOffline'

-- ************************************  SEARCHCATALOGTITLETAG  *******************************

-- Add any new rows to the search catalog
INSERT	dbo.SearchCatalogKeyword (KeywordID, Keyword)
SELECT	t.KeywordID, 
		t.Keyword
FROM	#tmpSearchCatalogKeyword t LEFT JOIN dbo.SearchCatalogKeyword s
			ON t.KeywordID = s.KeywordID
			AND t.Keyword = s.Keyword
WHERE	s.SearchCatalogKeywordID IS NULL

-- Remove any rows from the search catalog that no longer exist
DELETE	dbo.SearchCatalogKeyword
FROM	dbo.SearchCatalogKeyword s LEFT JOIN #tmpSearchCatalogKeyword t
			ON s.KeywordID = t.KeywordID
			AND s.Keyword = t.Keyword
WHERE	t.SearchCatalogKeywordID IS NULL

DROP TABLE #tmpSearchCatalogKeyword

-- ************************************  SEARCHCATALOGCREATOR  ********************************

-- Add any new rows to the search catalog
INSERT	dbo.SearchCatalogCreator (CreatorID, CreatorName)
SELECT	t.CreatorID, 
		t.CreatorName
FROM	#tmpSearchCatalogCreator t LEFT JOIN dbo.SearchCatalogCreator s
			ON t.CreatorID = s.CreatorID
			AND t.CreatorName = s.CreatorName COLLATE SQL_Latin1_General_CP1_CS_AS
WHERE	s.SearchCatalogCreatorID IS NULL

-- Remove any rows from the search catalog that no longer exist
DELETE	dbo.SearchCatalogCreator
-- select *
FROM	dbo.SearchCatalogCreator s LEFT JOIN #tmpSearchCatalogCreator t
			ON s.CreatorID = t.CreatorID
			AND s.CreatorName = t.CreatorName COLLATE SQL_Latin1_General_CP1_CS_AS
WHERE	t.SearchCatalogCreatorID IS NULL

DROP TABLE #tmpSearchCatalogCreator

-- ****************************************  SEARCHCATALOG  ***********************************

IF (@Target = 'Books' OR @Target = '')
BEGIN

	-- Add any new rows to the search catalog
	INSERT	dbo.SearchCatalog (TitleID, ItemID, FirstPageID, SearchText, FullTitle, 
					UniformTitle, PublicationDetails, PublisherPlace, PublisherName, 
					Volume, EditionStatement, Subjects, Associations, Variants, Authors,
					SearchAuthors, TitleContributors, ItemContributors, HasSegments, 
					HasLocalContent, HasExternalContent, HasIllustrations)
	SELECT	t.TitleID, 
			t.ItemID, 
			t.FirstPageID,
			t.SearchText, 
			t.FullTitle, 
			t.UniformTitle,
			t.PublicationDetails, 
			t.PublisherPlace, 
			t.PublisherName,
			t.Volume, 
			t.EditionStatement, 
			t.Subjects,
			t.Associations, 
			t.Variants, 
			t.Authors,
			t.SearchAuthors,
			t.TitleContributors,
			t.ItemContributors,
			t.HasSegments,
			t.HasLocalContent,
			t.HasExternalContent,
			t.HasIllustrations
	FROM	#tmpSearchCatalog t LEFT JOIN dbo.SearchCatalog s
				ON t.TitleID = s.TitleID
				AND t.ItemID = s.ItemID
	WHERE	s.SearchCatalogID IS NULL

	-- Update any existing rows in the search catalog that have changed
	UPDATE	dbo.SearchCatalog
	SET		FirstPageID = CASE WHEN ISNULL(s.FirstPageID, -1) <> ISNULL(t.FirstPageID, -1) THEN t.FirstPageID ELSE s.FirstPageID END,
			SearchText = CASE WHEN s.SearchText <> t.SearchText THEN t.SearchText ELSE s.SearchText END,
			FullTitle = CASE WHEN s.FullTitle <> t.FullTitle THEN t.FullTitle ELSE s.FullTitle END,
			UniformTitle = CASE WHEN s.UniformTitle <> t.UniformTitle THEN t.UniformTitle ELSE s.UniformTitle END,
			PublicationDetails = CASE WHEN s.PublicationDetails <> t.PublicationDetails THEN t.PublicationDetails ELSE s.PublicationDetails END,
			PublisherPlace = CASE WHEN s.PublisherPlace <> t.PublisherPlace THEN t.PublisherPlace ELSE s.PublisherPlace END,
			PublisherName = CASE WHEN s.PublisherName <> t.PublisherName THEN t.PublisherName ELSE s.PublisherName END,
			Volume = CASE WHEN s.Volume <> t.Volume THEN t.Volume ELSE s.Volume END,
			EditionStatement = CASE WHEN s.EditionStatement <> t.EditionStatement THEN t.EditionStatement ELSE s.EditionStatement END,
			Subjects = CASE WHEN s.Subjects <> t.Subjects THEN t.Subjects ELSE s.Subjects END,
			Associations = CASE WHEN s.Associations <> t.Associations THEN t.Associations ELSE s.Associations END,
			Variants = CASE WHEN s.Variants <> t.Variants THEN t.Variants ELSE s.Variants END,
			Authors = CASE WHEN s.Authors <> t.Authors THEN t.Authors ELSE s.Authors END,
			SearchAuthors = CASE WHEN s.SearchAuthors <> t.SearchAuthors THEN t.SearchAuthors ELSE s.SearchAuthors END,
			TitleContributors = CASE WHEN s.TitleContributors <> t.TitleContributors THEN t.TitleContributors ELSE s.TitleContributors END,
			ItemContributors = CASE WHEN s.ItemContributors <> t.ItemContributors THEN t.ItemContributors ELSE s.ItemContributors END,
			HasSegments = CASE WHEN s.HasSegments <> t.HasSegments THEN t.HasSegments ELSE s.HasSegments END,
			HasLocalContent = CASE WHEN s.HasLocalContent <> t.HasLocalContent THEN t.HasLocalContent ELSE s.HasLocalContent END,
			HasExternalContent = CASE WHEN s.HasExternalContent <> t.HasExternalContent THEN t.HasExternalContent ELSE s.HasExternalContent END,
			HasIllustrations = CASE WHEN s.HasIllustrations <> t.HasIllustrations THEN t.HasIllustrations ELSE s.HasIllustrations END,
			LastModifiedDate = GETDATE()
	FROM	dbo.SearchCatalog s INNER JOIN #tmpSearchCatalog t
				ON s.TitleID = t.TitleID
				AND s.ItemID = t.ItemID
	WHERE	ISNULL(s.FirstPageID, -1) <> ISNULL(t.FirstPageID, -1)
	OR		s.SearchText <> t.SearchText
	OR		s.FullTitle <> t.FullTitle
	OR		s.UniformTitle <> t.UniformTitle
	OR		s.PublicationDetails <> t.PublicationDetails
	OR		s.PublisherPlace <> t.PublisherPlace
	OR		s.PublisherName <> t.PublisherName
	OR		s.Volume <> t.Volume
	OR		s.EditionStatement <> t.EditionStatement
	OR		s.Subjects <> t.Subjects
	OR		s.Associations <> t.Associations
	OR		s.Variants <> t.Variants
	OR		s.Authors <> t.Authors
	OR		s.SearchAuthors <> t.SearchAuthors
	OR		s.TitleContributors <> t.TitleContributors
	OR		s.ItemContributors <> t.ItemContributors
	OR		s.HasSegments <> t.HasSegments
	OR		s.HasLocalContent <> t.HasLocalContent
	OR		s.HasExternalContent <> t.HasExternalContent
	OR		s.HasIllustrations <> t.HasIllustrations

	-- Remove any rows from the search catalog that no longer exist
	DELETE	dbo.SearchCatalog
	FROM	dbo.SearchCatalog s LEFT JOIN #tmpSearchCatalog t
				ON s.TitleID = t.TitleID
				AND s.ItemID = t.ItemID
	WHERE	t.SearchCatalogID IS NULL

	DROP TABLE #tmpSearchCatalog

END

-- ************************************  SEARCHCATALOGSEGMENT  ********************************

IF (@Target = 'Segments' OR @Target = '')
BEGIN

	-- Add any new rows to the search catalog
	INSERT	dbo.SearchCatalogSegment (SegmentID, ItemID, SearchText, Title, TranslatedTitle,
					ContainerTitle, PublicationDetails, Volume, Series, Issue, [Date],
					Subjects, Authors, SearchAuthors, Contributors, HasLocalContent, HasExternalContent,
					HasIllustrations)
	SELECT	t.SegmentID, 
			t.ItemID,
			t.SearchText, 
			t.Title, 
			t.TranslatedTitle,
			t.ContainerTitle,
			t.PublicationDetails, 
			t.Volume, 
			t.Series, 
			t.Issue,
			t.[Date],
			t.Subjects,
			t.Authors,
			t.SearchAuthors,
			t.Contributors,
			t.HasLocalContent,
			t.HasExternalContent,
			t.HasIllustrations
	FROM	#tmpSearchCatalogSegment t LEFT JOIN dbo.SearchCatalogSegment s
				ON t.SegmentID = s.SegmentID
	WHERE	s.SearchCatalogSegmentID IS NULL

	-- Update any existing rows in the search catalog that have changed
	UPDATE	dbo.SearchCatalogSegment
	SET		ItemID = CASE WHEN ISNULL(s.ItemID, -1) <> ISNULL(t.ItemID, -1) THEN t.ItemID ELSE s.ItemID END,
			SearchText = CASE WHEN s.SearchText <> t.SearchText THEN t.SearchText ELSE s.SearchText END,
			Title = CASE WHEN s.Title <> t.Title THEN t.Title ELSE s.Title END,
			TranslatedTitle = CASE WHEN s.TranslatedTitle <> t.TranslatedTitle THEN t.TranslatedTitle ELSE s.TranslatedTitle END,
			ContainerTitle = CASE WHEN s.ContainerTitle <> t.ContainerTitle THEN t.ContainerTitle ELSE s.ContainerTitle END,
			PublicationDetails = CASE WHEN s.PublicationDetails <> t.PublicationDetails THEN t.PublicationDetails ELSE s.PublicationDetails END,
			Volume = CASE WHEN s.Volume <> t.Volume THEN t.Volume ELSE s.Volume END,
			Series = CASE WHEN s.Series <> t.Series THEN t.Series ELSE s.Series END,
			Issue = CASE WHEN s.Issue <> t.Issue THEN t.Issue ELSE s.Issue END,
			[Date] = CASE WHEN s.[Date] <> t.[Date] THEN t.[Date] ELSE s.[Date] END,
			Subjects = CASE WHEN s.Subjects <> t.Subjects THEN t.Subjects ELSE s.Subjects END,
			Authors = CASE WHEN s.Authors <> t.Authors THEN t.Authors ELSE s.Authors END,
			SearchAuthors = CASE WHEN s.SearchAuthors <> t.SearchAuthors THEN t.SearchAuthors ELSE s.SearchAuthors END,
			Contributors = CASE WHEN s.Contributors <> t.Contributors THEN t.Contributors ELSE s.Contributors END,
			HasLocalContent = CASE WHEN s.HasLocalContent <> t.HasLocalContent THEN t.HasLocalContent ELSE s.HasLocalContent END,
			HasExternalContent = CASE WHEN s.HasExternalContent <> t.HasExternalContent THEN t.HasExternalContent ELSE s.HasExternalContent END,
			HasIllustrations = CASE WHEN s.HasIllustrations <> t.HasIllustrations THEN t.HasIllustrations ELSE s.HasIllustrations END,
			LastModifiedDate = GETDATE()
	FROM	dbo.SearchCatalogSegment s INNER JOIN #tmpSearchCatalogSegment t
				ON s.SegmentID = t.SegmentID
	WHERE	ISNULL(s.ItemID, -1) <> ISNULL(t.ItemID, -1)
	OR		s.SearchText <> t.SearchText
	OR		s.Title <> t.Title
	OR		s.TranslatedTitle <> t.TranslatedTitle
	OR		s.ContainerTitle <> t.ContainerTitle
	OR		s.PublicationDetails <> t.PublicationDetails
	OR		s.Volume <> t.Volume
	OR		s.Series <> t.Series
	OR		s.Issue <> t.Issue
	OR		s.[Date] <> t.[Date]
	OR		s.Subjects <> t.Subjects
	OR		s.Authors <> t.Authors
	OR		s.SearchAuthors <> t.SearchAuthors
	OR		s.Contributors <> t.Contributors
	OR		s.HasLocalContent <> t.HasLocalContent
	OR		s.HasExternalContent <> t.HasExternalContent
	OR		s.HasIllustrations <> t.HasIllustrations

	-- Remove any rows from the search catalog that no longer exist
	DELETE	dbo.SearchCatalogSegment
	FROM	dbo.SearchCatalogSegment s LEFT JOIN #tmpSearchCatalogSegment t
				ON s.SegmentID = t.SegmentID
	WHERE	t.SearchCatalogSegmentID IS NULL

	DROP TABLE #tmpSearchCatalogSegment

END

-- ********************************************************************************************
-- *************                              FINISH UP                           *************
-- ********************************************************************************************

-- Reorganize the full-text index to ensure optimum performance.
ALTER FULLTEXT CATALOG BHLSearchCatalog REORGANIZE

-- We're done updating, so mark the searchcatalog (full-text search) as online
UPDATE dbo.Configuration SET ConfigurationValue = 0 WHERE ConfigurationName = 'SearchCatalogOffline'

END
