
CREATE PROCEDURE [annotation].[SearchPageForConcept]

@AnnotationConceptCode nvarchar(20)

AS

BEGIN

SET NOCOUNT ON

-- Get pages related to concepts
SELECT	vac.ConceptText, at.TitleID, ai.ItemID, ap.PageID,
		t.ShortTitle, t.SortTitle, t.PublicationDetails, i.Volume, ti.ItemSequence,
		ip.PagePrefix, ip.PageNumber, ip.Implied, pt.PageTypeName, p.SequenceOrder
INTO	#Pages
FROM	annotation.vwAnnotationConcept vac WITH (NOLOCK)
		INNER JOIN annotation.Annotation_AnnotationConcept aac WITH (NOLOCK)
			ON vac.AnnotationConceptCode = aac.AnnotationConceptCode
		INNER JOIN annotation.PageAnnotation pa WITH (NOLOCK)
			ON aac.AnnotationID = pa.AnnotationID
		INNER JOIN annotation.AnnotatedPage ap WITH (NOLOCK)
			ON pa.AnnotatedPageID = ap.AnnotatedPageID
		INNER JOIN annotation.AnnotatedItem ai WITH (NOLOCK)
			ON ap.AnnotatedItemID = ai.AnnotatedItemID
		INNER JOIN annotation.AnnotatedTitle at WITH (NOLOCK)
			ON ai.AnnotatedTitleID = at.AnnotatedTitleID
		INNER JOIN dbo.Title t WITH (NOLOCK)
			ON at.TitleID = t.TitleID
		INNER JOIN dbo.Item i WITH (NOLOCK)
			ON ai.ItemID = i.ItemID
		INNER JOIN dbo.TitleItem ti WITH (NOLOCK)
			ON at.TitleID = ti.TitleID
			AND ai.ItemID = ti.ItemID
		INNER JOIN dbo.Page p WITH (NOLOCK)
			ON ap.PageID = p.PageID
		LEFT JOIN dbo.IndicatedPage ip WITH (NOLOCK)
			ON ap.PageID = ip.PageID
			AND ip.Sequence = 1
		LEFT JOIN dbo.Page_PageType ppt WITH (NOLOCK)
			ON ap.PageID = ppt.PageID
		LEFT JOIN dbo.PageType pt WITH (NOLOCK)
			ON ppt.PageTypeID = pt.PageTypeID
WHERE	ap.PageID IS NOT NULL
AND		vac.AnnotationConceptCode = @AnnotationConceptCode
AND		t.PublishReady = 1
AND		i.ItemStatusID = 40
AND		p.Active = 1
GROUP BY
		vac.ConceptText, at.TitleID, ai.ItemID, ap.PageID,
		t.ShortTitle, t.SortTitle, t.PublicationDetails, i.Volume, ti.ItemSequence, 
		ip.PagePrefix, ip.PageNumber, ip.Implied, pt.PageTypeName, p.SequenceOrder

SELECT	p.ConceptText, 
		p.TitleID, 
		p.ItemID, 
		p.PageID,
		p.ShortTitle, 
		p.PublicationDetails, 
		p.Volume, 
		p.PagePrefix, 
		p.PageNumber, 
		p.Implied, 
		p.PageTypeName,
		c.Authors
FROM	#Pages p
		INNER JOIN dbo.SearchCatalog c WITH (NOLOCK) ON p.TitleID = c.TitleID AND p.ItemID = c.ItemID
ORDER BY
		ConceptText, SortTitle, ItemSequence, SequenceOrder
		
END


