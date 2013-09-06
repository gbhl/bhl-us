

CREATE PROCEDURE [annotation].[GetRelatedPageByExternalIdentifier] 
	@ExternalIdentifier nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON

	SELECT	apt.AnnotatedPageTypeName AS AnnotatedPageTypeName, 
			ap.PageNumber AS PageNumber, 
			CASE WHEN ap.AnnotatedPageTypeID IN (1, 2, 7, 8, 10)  -- front/end notes/slips
			THEN
				CASE
				WHEN SUBSTRING(PageNumber, 2, 1) <> '0' AND SUBSTRING(PageNumber, 3, 2) = '00' THEN ' ' + SUBSTRING(PageNumber, 2, 1)
				WHEN SUBSTRING(PageNumber, 2, 1) <> '0' AND SUBSTRING(PageNumber, 3, 2) <> '00' THEN ' ' +SUBSTRING(PageNumber, 2, 1) + ',  Side ' + CONVERT(varchar(2), CONVERT(int, SUBSTRING(PageNumber, 3, 2)))
				WHEN SUBSTRING(PageNumber, 2, 1) = '0' AND SUBSTRING(PageNumber, 3, 2) <> '00' THEN ',  Side ' + CONVERT(varchar(2), CONVERT(int, SUBSTRING(PageNumber, 3, 2)))
				ELSE ''				
				END			
			ELSE PageNumber
			END AS ConvertedPageNumber,
			NULL AS PageID --ap.PageID
	FROM	annotation.AnnotatedPage ap	INNER JOIN annotation.AnnotatedPageType apt
				ON apt.AnnotatedPageTypeID = ap.AnnotatedPageTypeID
	WHERE	ap.ExternalIdentifier = @ExternalIdentifier
END




