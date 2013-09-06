
-- BibliographicLevelDeleteAuto PROCEDURE
-- Generated 8/3/2010 11:16:34 AM
-- Do not modify the contents of this procedure.
-- Delete Procedure for BibliographicLevel

CREATE PROCEDURE BibliographicLevelDeleteAuto

@BibliographicLevelID INT

AS 

DELETE FROM [dbo].[BibliographicLevel]

WHERE

	[BibliographicLevelID] = @BibliographicLevelID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure BibliographicLevelDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

