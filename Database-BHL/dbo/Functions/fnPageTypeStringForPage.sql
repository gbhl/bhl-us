
CREATE FUNCTION [dbo].[fnPageTypeStringForPage] 
(
	@PageID int
)
RETURNS nvarchar(1024)
AS
BEGIN
	
	DECLARE @PageTypeString nvarchar(1024)
/*
	DECLARE @NumRecords int

	SELECT @NumRecords = COUNT(*)
	FROM Page p
	INNER JOIN Page_PageType ppt ON (p.PageID = ppt.PageID)
	INNER JOIN PageType pt ON (ppt.PageTypeID = pt.PageTypeID)
	WHERE p.PageID = @PageID
*/
	DECLARE @CurrentRecord int
	SELECT @CurrentRecord = 1

	SELECT 
		@PageTypeString = COALESCE(@PageTypeString, '') +
					(CASE WHEN @CurrentRecord = 1 THEN ''
						ELSE ', ' END) + pt.PageTypeName,
		@CurrentRecord = @CurrentRecord + 1
	FROM Page p
	INNER JOIN Page_PageType ppt ON (p.PageID = ppt.PageID)
	INNER JOIN PageType pt ON (ppt.PageTypeID = pt.PageTypeID)
	WHERE p.PageID = @PageID
	--ORDER BY ip.Sequence ASC

	RETURN LTRIM(RTRIM(COALESCE(@PageTypeString, '')))
END
