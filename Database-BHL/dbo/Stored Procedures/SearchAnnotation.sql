
CREATE PROCEDURE [dbo].[SearchAnnotation]

@AnnotationText		nvarchar(200) = '',
@Title				nvarchar(2000) = '',
@AuthorLastName		nvarchar(255) = '',
@Volume				nvarchar(100) = '',
@Edition			nvarchar(450) = '',
@Year				smallint = NULL,
@CollectionID		int = null,
@AnnotationSourceID int = null,
@ReturnCount		int = 100

AS 

SET NOCOUNT ON

-- NOTE: The following queries have been broken down into multiple steps to 
-- best take advantage of the table indexes

-- Start by getting all matching annotations
SELECT	a.AnnotationTextClean AS AnnotationText,
		ap.PageID,
		ai.ItemID,
		at.TitleID,
		ti.ItemSequence
INTO	#tmpAnnotation
FROM	annotation.Annotation a INNER JOIN annotation.PageAnnotation pa
			ON a.AnnotationID = pa.AnnotationID
		INNER JOIN annotation.AnnotatedPage ap
			ON pa.AnnotatedPageID = ap.AnnotatedPageID
		INNER JOIN annotation.AnnotatedItem ai
			ON ap.AnnotatedItemID = ai.AnnotatedItemID
		INNER JOIN annotation.AnnotatedTitle at
			ON ai.AnnotatedTitleID = at.AnnotatedTitleID
		INNER JOIN dbo.TitleItem ti 
			ON at.TitleID = ti.TitleID AND ai.ItemID = ti.ItemID
WHERE	a.AnnotationTextClean LIKE '%' + @AnnotationText + '%'
AND		(at.AnnotationSourceID = @AnnotationSourceID OR @AnnotationSourceID IS NULL)
AND		ap.PageID IS NOT NULL

CREATE TABLE #tmpTitle
	(
	AnnotationText nvarchar(MAX) NOT NULL,
	TitleID int NOT NULL,
	ItemID int NOT NULL,
	PageID int NOT NULL,
	ItemSequence smallint NOT NULL
	)

-- Get initial list, filtered by title, language, and volume
INSERT	#tmpTitle
SELECT	a.AnnotationText, 
		a.TitleID, 
		a.ItemID,
		a.PageID,
		a.ItemSequence
FROM	#tmpAnnotation a INNER JOIN dbo.Title t ON a.TitleID = t.TitleID
		INNER JOIN dbo.Item i ON a.ItemID = i.ItemID
WHERE	t.FullTitle LIKE '%' + @Title + '%'
AND		t.PublishReady = 1
AND		i.ItemStatusID = 40
AND		(i.Volume LIKE '%' + @Volume + '%' OR @Volume = '')
AND		(t.EditionStatement LIKE '%' + @Edition + '%' OR @Edition = '')
AND		(@Year IS NULL OR 
			@Year BETWEEN ISNULL(t.StartYear, 0) and ISNULL(t.EndYear, 0) OR
			i.Year = CONVERT(nvarchar(20), @Year))

-- See if we selected anything.  If not, try without volume and edition (as those fields
-- contain much "dirty" data that may result in data being incorrectly excluded.)
IF NOT EXISTS(SELECT TitleID FROM #tmpTitle)
BEGIN
	INSERT	#tmpTitle	
	SELECT	a.AnnotationText, 
			a.TitleID, 
			a.ItemID,
			a.PageID,
			a.ItemSequence
	FROM	#tmpAnnotation a INNER JOIN dbo.Title t ON a.TitleID = t.TitleID
			INNER JOIN dbo.Item i ON a.ItemID = i.ItemID
			INNER JOIN dbo.TitleItem ti ON a.TitleID = ti.TitleID AND a.ItemID = ti.ItemID
	WHERE	t.FullTitle LIKE '%' + @Title + '%'
	AND		t.PublishReady = 1
	AND		i.ItemStatusID = 40
	AND		(@Year IS NULL OR 
				@Year BETWEEN ISNULL(t.StartYear, 0) and ISNULL(t.EndYear, 0) OR
				i.Year = CONVERT(nvarchar(20), @Year))
END

-- Filter by author
SELECT DISTINCT
		t.AnnotationText,
		t.TitleID,
		t.ItemID,
		t.PageID,
		t.ItemSequence
INTO	#tmpTitleFilter
FROM	#tmpTitle t 
		LEFT JOIN dbo.TitleAuthor ta ON t.TitleID = ta.TitleID
		LEFT JOIN dbo.Author a ON ta.AuthorID = a.AuthorID AND a.IsActive = 1
		LEFT JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID
WHERE	ISNULL(n.FullName, '') LIKE @AuthorLastName + '%' 
OR		@AuthorLastName = ''

-- Filter by collection
CREATE TABLE #tmpTitleFinal
	(
	AnnotationText nvarchar(MAX) NOT NULL,
	TitleID int NOT NULL,
	ItemID int NOT NULL,
	PageID int NOT NULL,
	ItemSequence smallint NOT NULL
	)

IF (@CollectionID IS NOT NULL)
BEGIN
	-- Get titles associated with a collection
	INSERT	#tmpTitleFinal
	SELECT	t.AnnotationText,
			t.TitleID,
			t.ItemID,
			t.PageID,
			t.ItemSequence
	FROM	#tmpTitleFilter t INNER JOIN dbo.TitleCollection tc ON t.TitleID = tc.TitleID
	WHERE	tc.CollectionID = @CollectionID

	-- Get items directly associated with a collection
	INSERT	#tmpTitleFinal
	SELECT	t.AnnotationText,
			t.TitleID,
			t.ItemID,
			t.PageID,
			t.ItemSequence
	FROM	#tmpTitleFilter t INNER JOIN dbo.ItemCollection ic ON t.ItemID = ic.ItemID
	WHERE	ic.CollectionID = @CollectionID
END
ELSE
BEGIN
	-- Just use everything when no collection specified
	INSERT	#tmpTitleFinal
	SELECT	t.AnnotationText,
			t.TitleID,
			t.ItemID,
			t.PageID,
			t.ItemSequence
	FROM	#tmpTitleFilter t 
END

-- Compile the final sortable result set
SELECT TOP (@ReturnCount)
		tmp.AnnotationText,
		tmp.TitleID,
		tmp.ItemID,
		tmp.PageID,
		tmp.ItemSequence,
		t.FullTitle,
		t.ShortTitle,
		t.SortTitle,
		t.PartNumber,
		t.PartName,
		t.EditionStatement,
		t.PublicationDetails,
		t.Datafield_260_a,
		t.Datafield_260_b,
		t.Datafield_260_c,
		i.Volume,
		c.Authors,
		dbo.fnIndicatedPageStringForPage(tmp.PageID) AS PageNumbers,
		dbo.fnPageTypeStringForPage(tmp.PageID) AS PageTypes
FROM	#tmpTitleFinal tmp INNER JOIN dbo.Title t ON tmp.TitleID = t.TitleID
		INNER JOIN dbo.Item i ON tmp.ItemID = i.ItemID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND i.ItemID = c.ItemID
ORDER BY
		t.SortTitle, tmp.ItemSequence
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SearchAnnotation. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END





