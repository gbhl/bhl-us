
CREATE VIEW [dbo].[vwIAPage]
AS
SELECT	p.ItemID, p.Sequence, s.PageNumber, s.PageType, 
		s.[Year], s.Volume, s.Issue, s.IssuePrefix, 
		p.LocalFileName, p.ExternalUrl
FROM	IAPage p LEFT JOIN IAScandata s
			ON p.ItemID = s.ItemID
			AND p.Sequence = s.Sequence
UNION
SELECT	s.ItemID, s.Sequence, s.PageNumber, s.PageType, 
		s.[Year], s.Volume, s.Issue, s.IssuePrefix, 
		NULL, NULL
FROM	IAScandata s LEFT JOIN IAPage p
			ON s.ItemID = p.ItemID
			AND s.Sequence = p.Sequence
WHERE	p.ItemID is null


