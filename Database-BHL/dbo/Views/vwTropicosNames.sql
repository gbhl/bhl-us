
CREATE VIEW [dbo].[vwTropicosNames]
AS
SELECT	NameID,
		BHLTitleID,
		FullName,
		NameNoAuthors,
		Rank,
		'http://www.biodiversitylibrary.org/page/' + CONVERT(varchar(20), BHLPageID) AS BHLPageUrl,
		'http://www.biodiversitylibrary.org/pageocr/' + CONVERT(varchar(20), BHLPageID) AS OCRPath
FROM	dbo.tropicosnames
WHERE	ISNULL(BHLPageID, 0) <> 0

