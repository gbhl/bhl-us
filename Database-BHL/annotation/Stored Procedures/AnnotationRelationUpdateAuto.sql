
-- AnnotationRelationUpdateAuto PROCEDURE
-- Generated 6/15/2010 1:29:40 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for AnnotationRelation

CREATE PROCEDURE annotation.AnnotationRelationUpdateAuto

@AnnotationID INT,
@RelatedExternalIdentifier NVARCHAR(50),
@Note NVARCHAR(MAX)

AS 

SET NOCOUNT ON

UPDATE [annotation].[AnnotationRelation]

SET

	[AnnotationID] = @AnnotationID,
	[RelatedExternalIdentifier] = @RelatedExternalIdentifier,
	[Note] = @Note,
	[LastModifiedDate] = getdate()

WHERE
	[AnnotationID] = @AnnotationID AND
	[RelatedExternalIdentifier] = @RelatedExternalIdentifier
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationRelationUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[AnnotationID],
		[RelatedExternalIdentifier],
		[Note],
		[CreationDate],
		[LastModifiedDate]

	FROM [annotation].[AnnotationRelation]
	
	WHERE
		[AnnotationID] = @AnnotationID AND 
		[RelatedExternalIdentifier] = @RelatedExternalIdentifier
	
	RETURN -- update successful
END

