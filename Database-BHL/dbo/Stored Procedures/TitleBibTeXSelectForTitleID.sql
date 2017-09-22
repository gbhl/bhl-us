
CREATE PROCEDURE [dbo].[TitleBibTeXSelectForTitleID]

@TitleID INT

AS
BEGIN

SET NOCOUNT ON

DECLARE @RedirID int
SELECT @RedirID = RedirectTitleID FROM dbo.Title WHERE TitleID = @TitleID

IF (@RedirID IS NOT NULL)
	exec dbo.TitleBibTeXSelectForTitleID @RedirID
ELSE
	SELECT	'bhl' + CONVERT(NVARCHAR(10), i.ItemID) AS CitationKey,
			'https://www.biodiversitylibrary.org/item/' + CONVERT(NVARCHAR(10), i.ItemID) AS Url,
			'https://www.biodiversitylibrary.org/bibliography/' + CONVERT(NVARCHAR(10), t.TitleID) AS Note,
			t.FullTitle + ' ' + ISNULL(t.PartNumber, '') + ' ' + ISNULL(t.PartName, '') AS Title, 
			ISNULL(t.Datafield_260_a, '') + ISNULL(t.Datafield_260_b, '') AS Publisher,
			CASE WHEN i.Year IS NULL THEN ISNULL(t.Datafield_260_c, '') ELSE i.Year END AS [Year],
			ISNULL(i.Volume, '') AS Volume , ISNULL(i.CopyrightStatus, '') AS CopyrightStatus,
			c.Authors,
			dbo.fnCOinSGetPageCountForItem(i.ItemID) AS Pages,
			c.Subjects AS Keywords
	FROM	dbo.Title t  WITH (NOLOCK)
			INNER JOIN dbo.TitleItem ti WITH (NOLOCK)
				ON t.TitleID = ti.TitleID
			INNER JOIN dbo.Item i WITH (NOLOCK)
				ON ti.ItemID = i.ItemID
			INNER JOIN dbo.SearchCatalog c WITH (NOLOCK)
				ON t.TitleID = c.TitleID
				AND i.ItemID = c.ItemID
	WHERE	t.PublishReady = 1
	AND		i.ItemStatusID = 40
	AND		t.TitleID = @TitleID
	ORDER BY ti.ItemSequence
			
END

