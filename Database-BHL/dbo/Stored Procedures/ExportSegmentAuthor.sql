CREATE PROCEDURE [dbo].[ExportSegmentAuthor]

AS

BEGIN

SET NOCOUNT ON

SELECT DISTINCT
		s.SegmentID,
		a.AuthorID,
		n.FullName + 
			CASE WHEN a.Numeration = '' THEN '' ELSE ' ' + a.Numeration END + 
			CASE WHEN a.Unit = '' THEN '' ELSE ' ' + a.Unit END + 
			CASE WHEN a.Title = '' THEN '' ELSE ' '  + a.Title END + 
			CASE WHEN a.Location = '' THEN '' ELSE ' ' + a.Location END  + 
			CASE WHEN n.FullerForm = '' THEN '' ELSE ' ' + n.FullerForm END + 
			CASE WHEN a.StartDate = '' THEN '' ELSE ' ' + a.StartDate + '-' END + a.EndDate AS CreatorName,
		CONVERT(NVARCHAR(16), MIN(ia.CreationDate), 120) AS CreationDate,
		MAX(scs.HasLocalContent) AS HasLocalContent,
		MAX(scs.HasExternalContent) AS HasExternalContent
FROM	dbo.vwSegment s
		INNER JOIN dbo.ItemAuthor ia ON s.ItemID = ia.ItemID
		INNER JOIN dbo.Author a ON ia.AuthorID = a.AuthorID
		INNER JOIN dbo.AuthorName n ON a.AuthorID = n.AuthorID AND n.IsPreferredName = 1
		INNER JOIN dbo.SearchCatalogSegment scs ON s.SegmentID = scs.SegmentID
		LEFT JOIN dbo.Book b ON s.BookID = b.BookID
		LEFT JOIN dbo.Item i ON b.ItemID = i.ItemID
WHERE	(i.ItemStatusID = 40 OR i.ItemStatusID IS NULL)
AND     (s.BookID IS NOT NULL OR ISNULL(s.Url, '') <> '')
AND		a.IsActive = 1
GROUP BY
		s.SegmentID,
		a.AuthorID,
		n.FullName + 
			CASE WHEN a.Numeration = '' THEN '' ELSE ' ' + a.Numeration END + 
			CASE WHEN a.Unit = '' THEN '' ELSE ' ' + a.Unit END + 
			CASE WHEN a.Title = '' THEN '' ELSE ' '  + a.Title END + 
			CASE WHEN a.Location = '' THEN '' ELSE ' ' + a.Location END  + 
			CASE WHEN n.FullerForm = '' THEN '' ELSE ' ' + n.FullerForm END + 
			CASE WHEN a.StartDate = '' THEN '' ELSE ' ' + a.StartDate + '-' END + a.EndDate

END

GO
