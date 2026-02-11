CREATE PROCEDURE [annotation].[AnnotationRelationSelectByAnnotationID] 

@AnnotationID int

AS
BEGIN
	SET NOCOUNT ON

	-- Relations are only stored in the database one way (supplements to paginated material).
	-- Therefore, we need to search slightly differently depending on whether our starting
	-- point is a supplement or paginated material.

	-- Look for paginated material related to supplementary material
	SELECT	a.AnnotationID, pa.AnnotatedPageID
	INTO	#tmpAnnotation
	FROM	annotation.AnnotationRelation r INNER JOIN annotation.Annotation a
				ON r.RelatedExternalIdentifier = a.ExternalIdentifier
			INNER JOIN annotation.PageAnnotation pa
				ON a.AnnotationID = pa.AnnotationID
	WHERE	r.AnnotationID = @AnnotationID
	UNION
	-- Look for supplementary material related to paginated material
	SELECT	r.AnnotationID, pa.AnnotatedPageID
	FROM	annotation.AnnotationRelation r INNER JOIN annotation.Annotation a
				ON r.RelatedExternalIdentifier = a.ExternalIdentifier
			INNER JOIN annotation.PageAnnotation pa
				ON r.AnnotationID = pa.AnnotationID
	WHERE	a.AnnotationID = @AnnotationID

	-- Get the pages related to the related annotations we've identified
	SELECT DISTINCT 
			a.AnnotationID,
			ap.PageID,
	        SUBSTRING(LTRIM(RTRIM(COALESCE(IndicatedPages.PageString, ''))), 1, 1024) AS IndicatedPage
	INTO	#Results
	FROM	#tmpAnnotation a 
			INNER JOIN annotation.AnnotatedPage ap ON a.AnnotatedPageID = ap.AnnotatedPageID
			INNER JOIN dbo.Page p ON ap.PageID = p.PageID
			-- Replaces fnIndicatedPageStringForPage
			CROSS APPLY (
				SELECT	STRING_AGG(
							ip2.PagePrefix + ' ' + 
							ISNULL(	CASE WHEN ip2.Implied = 1 
									THEN '[' + ip2.PageNumber + ']' 
									ELSE ip2.PageNumber END, ''),
							', ')
							WITHIN GROUP (ORDER BY ip2.Sequence ASC) AS PageString
				FROM	dbo.IndicatedPage ip2
				WHERE	ip2.PageID = p.PageID
			) AS IndicatedPages
	WHERE	ap.PageID IS NOT NULL
	
	SELECT	AnnotationID,
			PageID,
			CASE WHEN IndicatedPage = '' THEN 'Text' ELSE IndicatedPage END AS IndicatedPage
	FROM	#Results

END

GO
