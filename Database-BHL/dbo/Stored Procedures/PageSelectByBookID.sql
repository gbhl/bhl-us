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
        SUBSTRING(LTRIM(RTRIM(COALESCE(PageTypes.TypeString, ''))), 1, 1024) AS PageTypes,
        SUBSTRING(LTRIM(RTRIM(COALESCE(IndicatedPages.PageString, ''))), 1, 1024) AS IndicatedPages,
		pf.FlickrURL
FROM	[dbo].[Page] p
		INNER JOIN dbo.ItemPage ip ON p.PageID = ip.PageID
		JOIN [dbo].[Book] b on ip.ItemID = b.ItemID
		LEFT JOIN dbo.PageFlickr pf WITH (NOLOCK) ON p.PageID = pf.PageID
        -- Replaces fnIndicatedPageStringForPage
        CROSS APPLY (
            SELECT	STRING_AGG(
						ip2.PagePrefix + ' ' + 
							ISNULL(CASE WHEN ip2.Implied = 1 THEN '[' + ip2.PageNumber + ']'  ELSE ip2.PageNumber END, ''),
						', ')
						WITHIN GROUP (ORDER BY ip2.Sequence ASC) AS PageString
            FROM	dbo.IndicatedPage ip2
            WHERE	ip2.PageID = p.PageID
        ) AS IndicatedPages
        -- Replaces fnPageTypeStringForPage
        CROSS APPLY (
            SELECT	STRING_AGG(pt.PageTypeName, ', ') 
						WITHIN GROUP (ORDER BY pt.PageTypeName ASC) AS TypeString
            FROM	dbo.Page_PageType ppt
					INNER JOIN dbo.PageType pt ON ppt.PageTypeID = pt.PageTypeID
            WHERE	ppt.PageID = p.PageID
        ) AS PageTypes
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
