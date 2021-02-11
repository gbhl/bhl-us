CREATE VIEW dbo.vwPageFlickrNoteMachineTag

AS

WITH cte (TitleID, BookID, PageID, PageFlickrNoteID, PhotoID, FlickrNoteID, FlickrAuthorID,
			FlickrAuthorName, FlickrAuthorRealName, NoteValue, NoteValueClean, IsMachineTag, 
			MachineTagCount, IsActive, CreationDate, LastModifiedDate, DeleteDate) 
AS (
	 SELECT	it.TitleID,
			b.BookID,
			n.PageID,
			PageFlickrNoteID,
			PhotoID,
			FlickrNoteID,
			FlickrAuthorID,
			FlickrAuthorName,
			FlickrAuthorRealName,
			NoteValue,
			REPLACE(NoteValue, '&quot;', '') AS NoteValueClean,
			CASE 
				WHEN CHARINDEX(':', NoteValue) > 0 AND CHARINDEX('=', NoteValue) > CHARINDEX(':', NoteValue) 
				THEN 1 
				ELSE 0 
			END AS IsMachineTag,
			CASE
				WHEN CHARINDEX(CHAR(10), NoteValue) > 0 AND CHARINDEX(':', NoteValue) > 0 AND CHARINDEX('=', NoteValue) > 0 
				THEN LEN(NoteValue) - LEN(REPLACE(NoteValue, CHAR(10), '')) + 1
				WHEN CHARINDEX(':', NoteValue) > 0 AND CHARINDEX('=', NoteValue) > 0 
				THEN 1
				ELSE 0
			END AS MachineTagCount,
			IsActive,
			n.CreationDate,
			n.LastModifiedDate,
			n.DeleteDate
	FROM	dbo.PageFlickrNote n
			INNER JOIN BHLItemPage ip ON n.PageID = ip.PageID
			INNER JOIN BHLBook b ON ip.ItemID = b.ItemID
			INNER JOIN BHLTitleItem it ON b.ItemID = it.ItemID AND it.IsPrimary = 1
	WHERE	IsActive = 1
	)
SELECT	TitleID,
		BookID,
		PageID,
		PageFlickrNoteID, 
		PhotoID, 
		FlickrNoteID, 
		FlickrAuthorID,
		FlickrAuthorName, 
		FlickrAuthorRealName, 
		NoteValueClean AS NoteValue,
		CASE WHEN MachineTagCount = 1 THEN SUBSTRING(NoteValueClean, 1, CHARINDEX(':', NoteValueClean) - 1) ELSE '' END AS mtNameSpace,
		CASE WHEN MachineTagCount = 1 THEN SUBSTRING(NoteValueClean, CHARINDEX(':', NoteValueClean) + 1, CHARINDEX('=', NoteValueClean) - CHARINDEX(':', NoteValueClean) - 1) ELSE '' END AS mtPredicate, 
		CASE WHEN MachineTagCount = 1 THEN REVERSE(SUBSTRING(REVERSE(NoteValueClean), 1, CHARINDEX('=', REVERSE(NoteValueClean)) - 1)) ELSE '' END AS mtValue,
		MachineTagCount,
		CreationDate, 
		LastModifiedDate, 
		DeleteDate
FROM	cte
WHERE	IsMachineTag = 1
AND		IsActive = 1

GO

