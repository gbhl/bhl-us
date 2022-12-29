
CREATE FUNCTION [dbo].[fnIndicatedPageStringForPage] 
(
	@PageID int
)
RETURNS nvarchar(1024)
AS
BEGIN
	
	DECLARE @IndicatedPageString nvarchar(max)
	DECLARE @CurrentRecord int
	SELECT @CurrentRecord = 1

	SELECT 
		@IndicatedPageString = COALESCE(@IndicatedPageString, '') +
			(CASE WHEN @CurrentRecord = 1 THEN '' ELSE ', ' END) + 
			ip.PagePrefix + ' ' + 
			ISNULL((CASE WHEN ip.Implied = 1 THEN '[' + ip.PageNumber + ']' ELSE ip.PageNumber END), ''),
		@CurrentRecord = @CurrentRecord + 1
	FROM dbo.Page p
	INNER JOIN dbo.IndicatedPage ip ON (p.PageID = ip.PageID)
	WHERE p.PageID = @PageID
	ORDER BY ip.Sequence ASC

	RETURN SUBSTRING(LTRIM(RTRIM(COALESCE(@IndicatedPageString, ''))), 1, 1024)
END

GO
