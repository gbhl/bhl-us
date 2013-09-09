
-- IAMarcDataFieldDeleteAuto PROCEDURE
-- Generated 12/28/2007 12:46:28 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for IAMarcDataField

CREATE PROCEDURE IAMarcDataFieldDeleteAuto

@MarcDataFieldID INT

AS 

DELETE FROM [dbo].[IAMarcDataField]

WHERE

	[MarcDataFieldID] = @MarcDataFieldID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAMarcDataFieldDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

