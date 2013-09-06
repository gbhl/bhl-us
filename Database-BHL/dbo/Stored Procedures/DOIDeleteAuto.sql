
-- DOIDeleteAuto PROCEDURE
-- Generated 11/11/2011 1:11:27 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for DOI

CREATE PROCEDURE DOIDeleteAuto

@DOIID INT

AS 

DELETE FROM [dbo].[DOI]

WHERE

	[DOIID] = @DOIID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure DOIDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

