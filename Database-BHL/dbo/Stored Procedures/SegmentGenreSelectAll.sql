CREATE PROC dbo.SegmentGenreSelectAll

AS

BEGIN

SET NOCOUNT ON

SELECT	SegmentGenreID,
		GenreName,
		GenreDescription,
		CreationDate,
		LastModifiedDate,
		CreationUserID,
		LastModifiedUserID
FROM	dbo.SegmentGenre
ORDER BY
		GenreName

END

GO
