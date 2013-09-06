
CREATE PROCEDURE [annotation].[SearchPageForSubject]

@AnnotationSubjectCategoryID int,
@AnnotationSubjectID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @SubjectText nvarchar(150)
SELECT @SubjectText = SubjectText FROM annotation.AnnotationSubject WITH (NOLOCK) WHERE AnnotationSubjectID = @AnnotationSubjectID

-- Get pages related to concepts
SELECT	asubcat.SubjectCategoryName + ' - ' + asub.SubjectText AS SubjectText, 
		at.TitleID, ai.ItemID, ap.PageID,
		t.ShortTitle, t.SortTitle, t.PublicationDetails, i.Volume, ti.ItemSequence,
		ip.PagePrefix, ip.PageNumber, ip.Implied, pt.PageTypeName, p.SequenceOrder
INTO	#Pages		
FROM	annotation.AnnotationSubject asub  WITH (NOLOCK)
		INNER JOIN annotation.AnnotationSubjectCategory asubcat WITH (NOLOCK)
			ON asub.AnnotationSubjectCategoryID = asubcat.AnnotationSubjectCategoryID
		INNER JOIN annotation.PageAnnotation pa WITH (NOLOCK)
			ON asub.AnnotationID = pa.AnnotationID
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
AND		asub.SubjectText = @SubjectText
AND		asubcat.AnnotationSubjectCategoryID = @AnnotationSubjectCategoryID
AND		t.PublishReady = 1
AND		i.ItemStatusID = 40
AND		p.Active = 1
GROUP BY
		asubcat.SubjectCategoryName + ' - ' + asub.SubjectText, 
		at.TitleID, ai.ItemID, ap.PageID,
		t.ShortTitle, t.SortTitle, t.PublicationDetails, i.Volume, ti.ItemSequence, 
		ip.PagePrefix, ip.PageNumber, ip.Implied, pt.PageTypeName, p.SequenceOrder

SELECT	p.SubjectText, 
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
		SubjectText, SortTitle, ItemSequence, SequenceOrder

END



