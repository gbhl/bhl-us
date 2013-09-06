
CREATE PROCEDURE [annotation].[AnnotatedPageSelectByExternalIdentifer]

@ExternalIdentifier nvarchar(50),
@AnnotatedItemID int

AS

BEGIN

SELECT	AnnotatedPageID,
		AnnotatedItemID,
		PageID,
		ExternalIdentifier,
		AnnotatedPageTypeID,
		PageNumber,
		CreationDate,
		LastModifiedDate
FROM	annotation.AnnotatedPage
WHERE	ExternalIdentifier = @ExternalIdentifier
AND		AnnotatedItemID = @AnnotatedItemID

END

