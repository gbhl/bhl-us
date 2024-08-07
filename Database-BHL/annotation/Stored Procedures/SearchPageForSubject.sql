CREATE PROCEDURE [annotation].[SearchPageForSubject]

@AnnotationSubjectCategoryID int,
@AnnotationSubjectID int

AS

BEGIN

SET NOCOUNT ON

DECLARE @SubjectText nvarchar(150)
SELECT @SubjectText = SubjectText FROM annotation.AnnotationSubject WHERE AnnotationSubjectID = @AnnotationSubjectID

-- Get pages related to concepts
SELECT	asubcat.SubjectCategoryName + ' - ' + asub.SubjectText AS SubjectText, 
		at.TitleID, b.BookID, ai.ItemID, ap.PageID,
		t.ShortTitle, t.SortTitle, t.PublicationDetails, b.Volume, ti.ItemSequence,
		ip.PagePrefix, ip.PageNumber, ip.Implied, pt.PageTypeName, p.SequenceOrder
INTO	#Pages		
FROM	annotation.AnnotationSubject asub
		INNER JOIN annotation.AnnotationSubjectCategory asubcat ON asub.AnnotationSubjectCategoryID = asubcat.AnnotationSubjectCategoryID
		INNER JOIN annotation.PageAnnotation pa ON asub.AnnotationID = pa.AnnotationID
		INNER JOIN annotation.AnnotatedPage ap ON pa.AnnotatedPageID = ap.AnnotatedPageID
		INNER JOIN annotation.AnnotatedItem ai ON ap.AnnotatedItemID = ai.AnnotatedItemID
		INNER JOIN annotation.AnnotatedTitle at ON ai.AnnotatedTitleID = at.AnnotatedTitleID
		INNER JOIN dbo.Title t ON at.TitleID = t.TitleID
		INNER JOIN dbo.Item i ON ai.ItemID = i.ItemID
		INNER JOIN dbo.Book b ON i.ItemID = b.ItemID
		INNER JOIN dbo.ItemTitle ti ON at.TitleID = ti.TitleID AND ai.ItemID = ti.ItemID
		INNER JOIN dbo.Page p ON ap.PageID = p.PageID
		LEFT JOIN dbo.IndicatedPage ip ON ap.PageID = ip.PageID AND ip.Sequence = 1
		LEFT JOIN dbo.Page_PageType ppt ON ap.PageID = ppt.PageID
		LEFT JOIN dbo.PageType pt ON ppt.PageTypeID = pt.PageTypeID
WHERE	ap.PageID IS NOT NULL
AND		asub.SubjectText = @SubjectText
AND		asubcat.AnnotationSubjectCategoryID = @AnnotationSubjectCategoryID
AND		t.PublishReady = 1
AND		i.ItemStatusID = 40
AND		p.Active = 1
GROUP BY
		asubcat.SubjectCategoryName + ' - ' + asub.SubjectText, 
		at.TitleID, b.BookID, ai.ItemID, ap.PageID,
		t.ShortTitle, t.SortTitle, t.PublicationDetails, b.Volume, ti.ItemSequence, 
		ip.PagePrefix, ip.PageNumber, ip.Implied, pt.PageTypeName, p.SequenceOrder

SELECT	p.SubjectText, 
		p.TitleID, 
		p.BookID AS ItemID, 
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
		INNER JOIN dbo.SearchCatalog c ON p.TitleID = c.TitleID AND p.BookID = c.ItemID
ORDER BY
		SubjectText, SortTitle, ItemSequence, SequenceOrder

END

GO
