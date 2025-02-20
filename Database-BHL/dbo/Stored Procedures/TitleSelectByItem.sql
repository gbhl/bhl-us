CREATE PROCEDURE [dbo].[TitleSelectByItem]

@ItemID INT

AS

BEGIN

SET NOCOUNT ON;

SELECT	t.MARCBibID, 
		t.TitleID, 
		t.FullTitle,
		t.ShortTitle,
		t.PublicationDetails,
		t.BibliographicLevelID,
		t.PartNumber,
		t.PartName
FROM    dbo.Title t 
		INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID 
		INNER JOIN dbo.Item i ON it.ItemID = i.ItemID
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
WHERE	b.BookID = @ItemID
AND		t.PublishReady = 1
AND		i.ItemStatusID = 40

END

GO
