CREATE FUNCTION [dbo].[fnAPIIndicatedPageStringForPage] 
(
	@PageID int
)
RETURNS nvarchar(1024)
AS
BEGIN
	
	DECLARE @IndicatedPageString nvarchar(max)

	SELECT @IndicatedPageString = STUFF((
	
		SELECT '|' + ip.PagePrefix + (CASE WHEN ISNULL(ip.PageNumber, '') = '' THEN ip.PageNumber ELSE '%' + ip.PageNumber END)
		FROM Page p
		INNER JOIN IndicatedPage ip ON (p.PageID = ip.PageID)
		WHERE p.PageID = @PageID
		ORDER BY ip.Sequence ASC
		FOR XML PATH('')
		),1,1,'')

	RETURN SUBSTRING(LTRIM(RTRIM(COALESCE(@IndicatedPageString, ''))), 1, 1024)
END
