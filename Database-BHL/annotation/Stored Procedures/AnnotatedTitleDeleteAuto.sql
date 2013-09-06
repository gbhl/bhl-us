
-- AnnotatedTitleDeleteAuto PROCEDURE
-- Generated 7/14/2010 1:25:28 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for AnnotatedTitle

CREATE PROCEDURE [annotation].AnnotatedTitleDeleteAuto

@AnnotatedTitleID INT

AS 

DELETE FROM [annotation].[AnnotatedTitle]

WHERE

	[AnnotatedTitleID] = @AnnotatedTitleID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotatedTitleDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

