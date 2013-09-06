CREATE VIEW [annotation].[vwAnnotation]
AS
SELECT	at.AnnotatedTitleID, at.TitleID, at.Author, at.Title, at.PublicationDetails,
		ai.AnnotatedItemID, ai.ItemID,
		ap.AnnotatedPageID, ap.PageID, ap.ExternalIdentifier AS pExternalIdentifier,
		apt.AnnotatedPageTypeName, ap.PageNumber,
		a.AnnotationID, a.ExternalIdentifier AS aExternalIdentifier, a.SequenceNumber, 
		a.AnnotationText, a.AnnotationTextClean, a.AnnotationTextDisplay
FROM	annotation.AnnotatedTitle at INNER JOIN annotation.AnnotatedItem ai
			ON at.AnnotatedTitleID = ai.AnnotatedTitleID
		INNER JOIN annotation.AnnotatedPage ap
			ON ai.AnnotatedItemID = ap.AnnotatedItemID
		LEFT JOIN annotation.PageAnnotation pa
			ON ap.AnnotatedPageID = pa.AnnotatedPageID
		LEFT JOIN annotation.Annotation a
			ON pa.AnnotationID = a.AnnotationID
		INNER JOIN annotation.AnnotatedPageType apt
			ON ap.AnnotatedPageTypeID = apt.AnnotatedPageTypeID

