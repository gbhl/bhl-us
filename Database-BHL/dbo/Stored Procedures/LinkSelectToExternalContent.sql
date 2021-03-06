SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[LinkSelectToExternalContent]

AS

BEGIN

SET NOCOUNT ON

SELECT	'Item' AS Entity, 
		CONVERT(nvarchar(10), b.BookID) AS Id, 
		--Barcode COLLATE SQL_Latin1_General_CP1_CI_AI AS Title, 
		'' AS Title,
		ExternalUrl AS Url 
FROM	dbo.Book b
		INNER JOIN dbo.Item i ON b.ItemID = i.ItemID
WHERE	ISNULL(ExternalUrl, '') <> ''
AND		ItemStatusID = 40

UNION

SELECT	'Segment' AS Entity, 
		CONVERT(nvarchar(10), SegmentID) AS Id, 
		Title, 
		Url 
FROM	dbo.Segment s
		INNER JOIN dbo.Item i ON s.ItemID = i.ItemID
WHERE	ISNULL(Url, '') <> ''
AND		ItemStatusID IN (30, 40)

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


GO
