﻿CREATE PROCEDURE [import].[PageSelectByItemAndPageNumber]

@ItemID int,
@Volume nvarchar(20),
@PageNumber nvarchar(20)

AS

BEGIN

SET NOCOUNT ON

SELECT	p.PageID
FROM	dbo.IndicatedPage ip
		INNER JOIN dbo.Page p ON ip.PageID = p.PageID
		INNER JOIN dbo.Item i ON p.ItemID = i.ItemID
WHERE	p.ItemID = @ItemID
AND		p.Active = 1
AND		ip.PageNumber = @PageNumber
AND		(
		ip.PagePrefix IN ('', 'Page', 'p.') OR
		ip.PagePrefix LIKE '% Page'
		)
AND		(
		ISNULL(p.Volume, 'PGVOLNULL') = ISNULL(@Volume, 'VARNULL') OR
		(ISNULL(i.StartVolume, 'ITMVOLNULL') = ISNULL(@Volume, 'VARNULL') AND ISNULL(p.Volume, '') = '') OR
		(ISNULL(i.StartVolume, '') = '' AND ISNULL(p.Volume, '') = '') OR
		ISNULL(@Volume, '') = ''
		)

END