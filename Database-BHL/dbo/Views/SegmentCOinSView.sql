CREATE VIEW [dbo].[SegmentCOinSView]
AS
SELECT DISTINCT 
		s.SegmentID
		,s.Title AS 'rft_atitle'
		,s.ContainerTitle AS 'rft_jtitle'
		,s.[Date] as 'rft_date'
		,s.Volume AS 'rft_volume'
		,s.Issue AS 'rft_issue'
		,s.StartPageNumber AS 'rft_spage'
		,s.EndPageNumber AS 'rft_epage'
		,CASE WHEN s.PageRange <> '' THEN s.PageRange
			WHEN s.StartPageNumber <> '' AND s.EndPageNumber <> '' THEN s.StartPageNumber + '-' + s.EndPageNumber
			ELSE ''
		END AS 'rft_pages'
		,ISNULL(LOWER(s.LanguageCode), '') AS 'rft_language' -- dc
		,dbo.fnGetIdentifierForSegment(s.SegmentID, 'ISSN') AS 'rft_issn'
		,CASE WHEN CHARINDEX(',', dbo.fnCOinSGetFirstAuthorNameForSegment(s.SegmentID)) > 0
			THEN LTRIM(SUBSTRING(dbo.fnCOinSGetFirstAuthorNameForSegment(s.SegmentID), 1, CHARINDEX(',', dbo.fnCOinSGetFirstAuthorNameForSegment(s.SegmentID)) - 1))
			ELSE ISNULL(dbo.fnCOinSGetFirstAuthorNameForSegment(s.SegmentID), '')
		END AS 'rft_aulast'
		,CASE WHEN CHARINDEX(',', dbo.fnCOinSGetFirstAuthorNameForSegment(s.SegmentID)) > 0
			THEN LTRIM(REPLACE(SUBSTRING(dbo.fnCOinSGetFirstAuthorNameForSegment(s.SegmentID), CHARINDEX(',', dbo.fnCOinSGetFirstAuthorNameForSegment(s.SegmentID)), LEN(dbo.fnCOinSGetFirstAuthorNameForSegment(s.SegmentID)) - CHARINDEX(',', dbo.fnCOinSGetFirstAuthorNameForSegment(s.SegmentID)) + 1), ',', ''))
			ELSE ''
		END AS 'rft_aufirst'
		,dbo.fnCOinSAuthorStringForSegment(s.SegmentID) AS 'rft_au'
		,dbo.fnKeywordStringForSegment(s.SegmentID) AS 'rft_subject' -- dc
		,dbo.fnGetIdentifierForSegment(s.SegmentID, 'ISBN') AS 'rft_isbn'
		,dbo.fnGetIdentifierForSegment(s.SegmentID, 'CODEN') AS 'rft_coden'
		,LOWER(g.GenreName) AS 'rft_genre'
		,i.InstitutionName AS 'rft_contributor' -- dc
FROM	dbo.Segment s
		LEFT JOIN dbo.Institution i ON s.ContributorCode = i.InstitutionCode
		INNER JOIN dbo.SegmentGenre g ON s.SegmentGenreID = g.SegmentGenreID
WHERE	s.SegmentStatusID IN (10, 20)
AND		g.GenreName IN ('Article', 'Issue', 'Proceeding', 'Conference', 'Preprint', 'Unknown')

