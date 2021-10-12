CREATE PROCEDURE [dbo].[PageSelectByBookID]

@BookID INT

AS 

SET NOCOUNT ON

SELECT 	p.[PageID],
		b.BookID AS [ItemID],
		[FileNamePrefix],
		ip.[SequenceOrder],
		[Illustration],
		[Active],
		[Year],
		[Series],
		p.[Volume],
		[Issue],
		p.[ExternalURL],
		[IssuePrefix],
		dbo.fnPageTypeStringForPage(p.PageID) AS PageTypes,
		dbo.fnIndicatedPageStringForPage(p.PageID) AS IndicatedPages,
		pf.FlickrURL
FROM	[dbo].[Page] p
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		JOIN [dbo].[Book] b on ip.ItemID = b.ItemID
		LEFT JOIN dbo.PageFlickr pf WITH (NOLOCK) ON p.PageID = pf.PageID
WHERE	b.BookID = @BookID
AND		p.Active = 1
ORDER BY ip.[SequenceOrder] ASC

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageSelectByBookID. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
