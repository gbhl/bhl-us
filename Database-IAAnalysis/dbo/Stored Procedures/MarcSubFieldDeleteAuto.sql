
-- MarcSubFieldDeleteAuto PROCEDURE
-- Generated 11/12/2008 3:12:29 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for MarcSubField

CREATE PROCEDURE MarcSubFieldDeleteAuto

@MarcSubFieldID INT

AS 

DELETE FROM [dbo].[MarcSubField]

WHERE

	[MarcSubFieldID] = @MarcSubFieldID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure MarcSubFieldDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

