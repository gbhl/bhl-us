CREATE PROCEDURE dbo.ExportSegmentAuthor

AS

BEGIN

SET NOCOUNT ON

SELECT	sa.SegmentID,
		n.FullName + 
			CASE WHEN a.Numeration = '' THEN '' ELSE ' ' + a.Numeration END + 
			CASE WHEN a.Unit = '' THEN '' ELSE ' ' + a.Unit END + 
			CASE WHEN a.Title = '' THEN '' ELSE ' '  + a.Title END + 
			CASE WHEN a.Location = '' THEN '' ELSE ' ' + a.Location END  + 
			CASE WHEN n.FullerForm = '' THEN '' ELSE ' ' + n.FullerForm END + 
			CASE WHEN a.StartDate = '' THEN '' ELSE ' ' + a.StartDate + '-' END + a.EndDate AS CreatorName,
		CONVERT(NVARCHAR(16), sa.CreationDate, 120) AS CreationDate
FROM	dbo.SegmentAuthor sa WITH (NOLOCK)
		INNER JOIN dbo.Segment s WITH (NOLOCK) ON sa.SegmentID = s.SegmentID
		INNER JOIN dbo.Author a WITH (NOLOCK) ON sa.AuthorID = a.AuthorID
		INNER JOIN dbo.AuthorName n WITH (NOLOCK) ON a.AuthorID = n.AuthorID AND n.IsPreferredName = 1
WHERE	s.SegmentStatusID IN (10, 20)
ORDER BY
		s.SegmentID,
		sa.SequenceOrder

END

