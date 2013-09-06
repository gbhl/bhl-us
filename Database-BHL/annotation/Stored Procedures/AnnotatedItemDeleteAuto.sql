
-- AnnotatedItemDeleteAuto PROCEDURE
-- Generated 7/14/2010 1:25:28 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for AnnotatedItem

CREATE PROCEDURE [annotation].AnnotatedItemDeleteAuto

@AnnotatedItemID INT

AS 

DELETE FROM [annotation].[AnnotatedItem]

WHERE

	[AnnotatedItemID] = @AnnotatedItemID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotatedItemDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

