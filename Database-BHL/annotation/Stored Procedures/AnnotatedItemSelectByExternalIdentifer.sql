CREATE PROCEDURE [annotation].[AnnotatedItemSelectByExternalIdentifer]

@ExternalIdentifier nvarchar(50),
@AnnotatedTitleID int

AS

BEGIN

SELECT	AnnotatedItemID,
		AnnotatedTitleID,
		ItemID,
		ExternalIdentifier,
		Volume,
		CreationDate,
		LastModifiedDate
FROM	annotation.AnnotatedItem
WHERE	ExternalIdentifier = @ExternalIdentifier
AND		AnnotatedTitleID = @AnnotatedTitleID

END

