
-- AnnotationRelationDeleteAuto PROCEDURE
-- Generated 6/15/2010 1:29:40 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for AnnotationRelation

CREATE PROCEDURE annotation.AnnotationRelationDeleteAuto

@AnnotationID INT,
@RelatedExternalIdentifier NVARCHAR(50)

AS 

DELETE FROM [annotation].[AnnotationRelation]

WHERE

	[AnnotationID] = @AnnotationID AND
	[RelatedExternalIdentifier] = @RelatedExternalIdentifier

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationRelationDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

