
-- AnnotatedPageDeleteAuto PROCEDURE
-- Generated 7/14/2010 1:25:28 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for AnnotatedPage

CREATE PROCEDURE [annotation].AnnotatedPageDeleteAuto

@AnnotatedPageID INT

AS 

DELETE FROM [annotation].[AnnotatedPage]

WHERE

	[AnnotatedPageID] = @AnnotatedPageID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotatedPageDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

