CREATE PROCEDURE dbo.LinkSelectToExternalContent

AS

BEGIN

SET NOCOUNT ON

SELECT	'Item' AS Entity, 
		CONVERT(nvarchar(10), ItemID) AS Id, 
		--Barcode COLLATE SQL_Latin1_General_CP1_CI_AI AS Title, 
		'' AS Title,
		ExternalUrl AS Url 
FROM	dbo.Item 
WHERE	ISNULL(ExternalUrl, '') <> ''
AND		ItemStatusID = 40

UNION

SELECT	'Segment' AS Entity, 
		CONVERT(nvarchar(10), SegmentID) AS Id, 
		Title, 
		Url 
FROM	dbo.Segment
WHERE	ISNULL(Url, '') <> ''
AND		SegmentStatusID IN (10, 20)

UNION

SELECT	'Content Provider' AS Entity, 
		InstitutionCode AS Id, 
		InstitutionName COLLATE SQL_Latin1_General_CP1_CI_AI AS Title, 
		InstitutionUrl AS Url 
FROM	dbo.Institution 
WHERE	ISNULL(InstitutionUrl, '') <> ''

UNION

SELECT	'Title' AS Entity, 
		CONVERT(nvarchar(10), ti.TitleID) AS Id, 
		ShortTitle AS Title, 
		Url 
FROM	dbo.TitleInstitution ti 
		INNER JOIN dbo.Title t ON ti.TitleID = t.TitleID
WHERE	ISNULL(Url, '') <> ''
AND		t.PublishReady = 1

ORDER BY Entity, Id

END


