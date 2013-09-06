
-- MarcSubFieldDeleteAuto PROCEDURE
-- Generated 4/15/2009 3:34:26 PM
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

