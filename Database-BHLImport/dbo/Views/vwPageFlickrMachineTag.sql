CREATE VIEW dbo.vwPageFlickrMachineTag

AS

SELECT	it.TitleID,
		b.BookID,
		b.ItemID,
		f.PageID,
		PageFlickrTagID,
		PhotoID,
		IsMachineTag, 
		TagValue, 
		CASE WHEN CHARINDEX(':', TagValue) > 0 THEN SUBSTRING(TagValue, 1, CHARINDEX(':', TagValue) - 1) ELSE '' END AS mtNameSpace,
		CASE WHEN CHARINDEX(':', TagValue) > 0  AND CHARINDEX('=', TagValue) > 0 THEN SUBSTRING(TagValue, CHARINDEX(':', TagValue) + 1, CHARINDEX('=', TagValue) - CHARINDEX(':', TagValue) - 1) ELSE '' END AS mtPredicate, 
		CASE WHEN CHARINDEX('=', TagValue) > 0 THEN REVERSE(SUBSTRING(REVERSE(TagValue), 1, CHARINDEX('=', REVERSE(TagValue)) - 1)) ELSE '' END AS mtValue,
		FlickrAuthorID,
		FLickrAuthorName,
		IsActive,
		f.CreationDate,
		f.LastModifiedDate,
		DeleteDate
FROM	dbo.PageFlickrTag f
		INNER JOIN BHLItemPage ip ON f.PageID = ip.PageID
		INNER JOIN BHLBook b ON ip.ItemID = b.ItemID
		INNER JOIN BHLTitleItem it ON b.ItemID = it.ItemID AND it.IsPrimary = 1
WHERE	IsMachineTag = 1
AND		IsActive = 1
AND		CHARINDEX(':', TagValue) > 0
AND		CHARINDEX('=', TagValue) > 0

GO

