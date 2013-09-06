
-- AnnotatedPageTypeDeleteAuto PROCEDURE
-- Generated 12/20/2010 4:03:38 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for AnnotatedPageType

CREATE PROCEDURE [annotation].AnnotatedPageTypeDeleteAuto

@AnnotatedPageTypeID INT

AS 

DELETE FROM [annotation].[AnnotatedPageType]

WHERE

	[AnnotatedPageTypeID] = @AnnotatedPageTypeID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotatedPageTypeDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

