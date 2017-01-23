CREATE PROCEDURE [dbo].[PageSelectByItemID]

@ItemID INT

AS 

SET NOCOUNT ON

SELECT 	p.[PageID],
		[ItemID],
		[FileNamePrefix],
		[SequenceOrder],
		[Illustration],
		[Active],
		[Year],
		[Series],
		[Volume],
		[Issue],
		[ExternalURL],
		[IssuePrefix],
		dbo.fnPageTypeStringForPage(p.PageID) AS PageTypes,
		dbo.fnIndicatedPageStringForPage(p.PageID) AS IndicatedPages,
		pf.FlickrURL
FROM	[dbo].[Page] p
		LEFT JOIN dbo.PageFlickr pf WITH (NOLOCK) ON p.PageID = pf.PageID
WHERE	[ItemID] = @ItemID
ORDER BY [SequenceOrder] ASC

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageSelectByItemID. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
