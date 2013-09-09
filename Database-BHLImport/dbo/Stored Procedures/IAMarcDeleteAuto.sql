
-- IAMarcDeleteAuto PROCEDURE
-- Generated 12/28/2007 12:46:28 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for IAMarc

CREATE PROCEDURE IAMarcDeleteAuto

@MarcID INT

AS 

DELETE FROM [dbo].[IAMarc]

WHERE

	[MarcID] = @MarcID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAMarcDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

