
-- NameResolvedDeleteAuto PROCEDURE
-- Generated 12/10/2012 3:05:47 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for NameResolved

CREATE PROCEDURE NameResolvedDeleteAuto

@NameResolvedID INT

AS 

DELETE FROM [dbo].[NameResolved]

WHERE

	[NameResolvedID] = @NameResolvedID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure NameResolvedDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

