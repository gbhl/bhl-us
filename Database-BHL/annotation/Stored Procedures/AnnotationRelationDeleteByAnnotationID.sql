CREATE PROC [annotation].[AnnotationRelationDeleteByAnnotationID]

@AnnotationID int

AS

BEGIN

DELETE	
FROM	annotation.AnnotationRelation
WHERE	AnnotationID = @AnnotationID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationRelationDeleteByAnnotationID. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

END


