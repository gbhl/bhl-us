
-- IAFileDeleteAuto PROCEDURE
-- Generated 8/23/2010 3:08:23 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for IAFile

CREATE PROCEDURE IAFileDeleteAuto

@FileID INT

AS 

DELETE FROM [dbo].[IAFile]

WHERE

	[FileID] = @FileID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAFileDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

