CREATE PROCEDURE [dbo].[ExportTitle]

AS

BEGIN

SET NOCOUNT ON

SELECT	t.TitleID,
		it.ItemID,
		t.MARCBibID, 
		t.MARCLeader, 
		CONVERT(NVARCHAR(4000), t.FullTitle) AS FullTitle, 
		t.ShortTitle, 
		t.PartNumber,
		t.PartName,
		t.PublicationDetails, 
		t.CallNumber, 
		t.StartYear, 
		t.EndYear, 
		t.LanguageCode, 
		t.TL2Author, 
		'https://www.biodiversitylibrary.org/bibliography/' + CONVERT(nvarchar(20), t.TitleID) AS TitleURL, 
		MAX(c.HasLocalContent) AS HasLocalContent,
		MAX(c.HasExternalContent) AS HasExternalContent,
		CONVERT(nvarchar(16), t.CreationDate, 120) AS CreationDate
INTO	#title
FROM	dbo.Title t
		INNER JOIN dbo.ItemTitle it ON t.TitleID = it.TitleID
		INNER JOIN dbo.Book b ON it.ItemID = b.ItemID
		INNER JOIN dbo.SearchCatalog c ON t.TitleID = c.TitleID AND b.BookID = c.ItemID
GROUP BY
		t.TitleID, 
		it.ItemID,
		t.MARCBibID, 
		t.MARCLeader, 
		t.FullTitle, 
		t.ShortTitle, 
		t.PartNumber,
		t.PartName,
		t.PublicationDetails, 
		t.CallNumber, 
		t.StartYear, 
		t.EndYear, 
		t.LanguageCode, 
		t.TL2Author, 
		t.CreationDate;

-- Check titles with no local content to make sure there are no related virtual issues that DO have local content
WITH TitleCTE (TitleID, ItemID, HasLocalContent)  
AS  
(  
	SELECT	t.TitleID, t.ItemID, MAX(c.HasLocalContent)
	FROM	#title t
			INNER JOIN dbo.ItemRelationship ir ON t.ItemID = ir.ParentID
			INNER JOIN dbo.vwSegment s ON ir.ChildID = s.ItemID
			INNER JOIN dbo.SearchCatalogSegment c ON s.SegmentID = c.SegmentID
	WHERE	t.HasLocalContent = 0
	GROUP BY t.TitleID, t.ItemID
)  
UPDATE	#title
SET		HasLocalContent = cte.HasLocalContent
FROM	#title t INNER JOIN TitleCTE cte ON t.TitleID = cte.TitleID AND t.ItemID = cte.ItemID;

SELECT	TitleID,
		MARCBibID, 
		MARCLeader, 
		FullTitle, 
		ShortTitle, 
		PartNumber,
		PartName,
		PublicationDetails, 
		CallNumber, 
		StartYear, 
		EndYear, 
		LanguageCode, 
		TL2Author, 
		TitleURL, 
		MAX(HasLocalContent) AS HasLocalContent,
		MAX(HasExternalContent) AS HasExternalContent,
		CreationDate
FROM	#title
GROUP BY
		TitleID, 
		MARCBibID, 
		MARCLeader, 
		FullTitle, 
		ShortTitle, 
		PartNumber,
		PartName,
		PublicationDetails, 
		CallNumber, 
		StartYear, 
		EndYear, 
		LanguageCode,
		TitleURL,
		TL2Author, 
		CreationDate;
		
END

GO
