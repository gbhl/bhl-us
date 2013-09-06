﻿CREATE PROC [annotation].[Annotation_AnnotationConceptDeleteByAnnotationID]

@AnnotationID int

AS

BEGIN

DELETE	
FROM	annotation.Annotation_AnnotationConcept
WHERE	AnnotationID = @AnnotationID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure Annotation_AnnotationConceptDeleteByAnnotationID. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

END



