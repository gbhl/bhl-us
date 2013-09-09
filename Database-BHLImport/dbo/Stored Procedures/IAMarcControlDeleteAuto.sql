
-- IAMarcControlDeleteAuto PROCEDURE
-- Generated 7/8/2013 2:53:08 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for IAMarcControl

CREATE PROCEDURE IAMarcControlDeleteAuto

@MarcControlID INT

AS 

DELETE FROM [dbo].[IAMarcControl]

WHERE

	[MarcControlID] = @MarcControlID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAMarcControlDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

