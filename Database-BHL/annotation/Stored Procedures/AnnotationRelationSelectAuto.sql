
-- AnnotationRelationSelectAuto PROCEDURE
-- Generated 6/15/2010 1:29:40 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for AnnotationRelation

CREATE PROCEDURE annotation.AnnotationRelationSelectAuto

@AnnotationID INT,
@RelatedExternalIdentifier NVARCHAR(50)

AS 

SET NOCOUNT ON

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

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotationRelationSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

