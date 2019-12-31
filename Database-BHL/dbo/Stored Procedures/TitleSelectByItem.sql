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
		t.BibliographicLevelID
FROM    Title t INNER JOIN TitleItem ti
			ON t.TitleID = ti.TitleID 
		INNER JOIN Item i
			ON ti.ItemID = i.ItemID
WHERE	i.ItemID = @ItemID
AND		t.PublishReady = 1
AND		i.ItemStatusID = 40

END
