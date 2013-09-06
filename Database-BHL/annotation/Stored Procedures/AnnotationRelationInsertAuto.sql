
-- AnnotationRelationInsertAuto PROCEDURE
-- Generated 6/15/2010 1:29:40 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for AnnotationRelation

CREATE PROCEDURE annotation.AnnotationRelationInsertAuto

@AnnotationID INT,
@RelatedExternalIdentifier NVARCHAR(50),
@Note NVARCHAR(MAX)

AS 

SET NOCOUNT ON

INSERT INTO [annotation].[AnnotationRelation]
(
	[AnnotationID],
	[RelatedExternalIdentifier],
	[Note],
	[CreationDate],
	[LastModifiedDate]
)
VALUES
(
	@AnnotationID,
	@RelatedExternalIdentifier,
	@Note,
	getdate(),
	getdate()
)

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationRelationInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

