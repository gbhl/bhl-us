CREATE PROCEDURE import.ImportFileSelectById

@ImportFileID INT

AS 

SET NOCOUNT ON

SELECT	ImportFileID,
		ImportFileStatusID,
		ImportFileName,
		ContributorCode,
		f.SegmentGenreID,
		g.GenreName,
		f.CreationDate
FROM	import.ImportFile f
		INNER JOIN dbo.SegmentGenre g on f.SegmentGenreID = g.SegmentGenreID
WHERE	ImportFileID = @ImportFileID