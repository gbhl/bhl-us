
-- CreatorDeleteAuto PROCEDURE
-- Generated 4/4/2008 9:03:06 AM
-- Do not modify the contents of this procedure.
-- Delete Procedure for Creator

CREATE PROCEDURE CreatorDeleteAuto

@CreatorID INT

AS 

DELETE FROM [dbo].[Creator]

WHERE

	[CreatorID] = @CreatorID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure CreatorDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

