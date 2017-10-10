
CREATE VIEW [dbo].[vwTropicosNames]
AS
SELECT	NameID,
		BHLTitleID,
		FullName,
		NameNoAuthors,
		Rank,
		'https://www.biodiversitylibrary.org/page/' + CONVERT(varchar(20), BHLPageID) AS BHLPageUrl,
		'https://www.biodiversitylibrary.org/pageocr/' + CONVERT(varchar(20), BHLPageID) AS OCRPath
FROM	dbo.tropicosnames
WHERE	ISNULL(BHLPageID, 0) <> 0

