
CREATE PROCEDURE [annotation].[AnnotatedTitleSelectByExternalIdentifer]

@ExternalIdentifier nvarchar(50),
@AnnotationSourceID int

AS

BEGIN

SELECT	AnnotatedTitleID,
		AnnotationSourceID,
		TitleID,
		ExternalIdentifier,
		Author,
		Title,
		Edition,
		Volume,
		PublicationDetails,
		[Date],
		Location,
		IsBeagleEra,
		Inscription,
		CreationDate,
		LastModifiedDate
FROM	annotation.AnnotatedTitle
WHERE	ExternalIdentifier = @ExternalIdentifier
AND		AnnotationSourceID = @AnnotationSourceID

END




