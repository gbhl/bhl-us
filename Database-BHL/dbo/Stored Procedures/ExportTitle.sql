CREATE PROCEDURE dbo.ExportTitle

AS

BEGIN

SET NOCOUNT ON

SELECT DISTINCT 
		t.TitleID, 
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
		'http://www.biodiversitylibrary.org/title/' + CONVERT(nvarchar(20), t.TitleID) AS TitleURL, 
		CONVERT(nvarchar(16), t.CreationDate, 120) AS CreationDate
FROM	dbo.Title t WITH (NOLOCK)
		INNER JOIN dbo.Item i WITH (NOLOCK) ON t.TitleID = i.PrimaryTitleID
WHERE	t.PublishReady = 1

END

