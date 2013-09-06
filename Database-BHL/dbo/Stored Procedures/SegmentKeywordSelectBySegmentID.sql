
CREATE PROCEDURE [dbo].[SegmentKeywordSelectBySegmentID]

@SegmentID int

AS

BEGIN

SELECT	sk.SegmentKeywordID,
		sk.SegmentID,
		sk.KeywordID,
		k.Keyword,
		sk.CreationDate,
		sk.LastModifiedDate,
		sk.CreationUserID,
		sk.LastModifiedUserID
FROM	dbo.SegmentKeyword sk INNER JOIN dbo.Keyword k
			ON sk.KeywordID = k.KeywordID
WHERE	SegmentID = @SegmentID
ORDER BY
		k.Keyword

END


