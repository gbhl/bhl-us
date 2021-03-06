SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ExportTitle]

AS

BEGIN

SET NOCOUNT ON

SELECT	t.TitleID, 
		t.MARCBibID, 
		t.MARCLeader, 
		CONVERT(NVARCHAR(4000), t.FullTitle) AS FullTitle, 
		t.ShortTitle, 
		t.PublicationDetails, 
		t.CallNumber, 
		t.StartYear, 
		t.EndYear, 
		t.LanguageCode, 
		t.TL2Author, 
		'https://www.biodiversitylibrary.org/title/' + CONVERT(nvarchar(20), t.TitleID) AS TitleURL, 
		MAX(c.HasLocalContent) AS HasLocalContent,
		MAX(c.HasExternalContent) AS HasExternalContent,
		CONVERT(nvarchar(16), t.CreationDate, 120) AS CreationDate
FROM	dbo.Title t WITH (NOLOCK)
		INNER JOIN dbo.vwItemPrimaryTitle pt WITH (NOLOCK) ON t.TitleID = pt.TitleID
		INNER JOIN dbo.Book b WITH (NOLOCK) ON pt.ItemID = b.ItemID
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
WHERE	t.PublishReady = 1
GROUP BY
		t.TitleID, 
		t.MARCBibID, 
		t.MARCLeader, 
		t.FullTitle, 
		t.ShortTitle, 
		t.PublicationDetails, 
		t.CallNumber, 
		t.StartYear, 
		t.EndYear, 
		t.LanguageCode, 
		t.TL2Author, 
		t.CreationDate
		
END


GO
