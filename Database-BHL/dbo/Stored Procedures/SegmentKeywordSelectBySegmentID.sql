SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SegmentKeywordSelectBySegmentID]

@SegmentID int

AS

BEGIN

SELECT	ik.ItemKeywordID,
		s.SegmentID,
		ik.ItemID,
		ik.KeywordID,
		k.Keyword,
		ik.CreationDate,
		ik.LastModifiedDate,
		ik.CreationUserID,
		ik.LastModifiedUserID
FROM	dbo.Segment s
		INNER JOIN dbo.ItemKeyword ik ON s.ItemID = ik.ItemID
		INNER JOIN dbo.Keyword k ON ik.KeywordID = k.KeywordID
WHERE	s.SegmentID = @SegmentID
ORDER BY
		k.Keyword

END


GO
