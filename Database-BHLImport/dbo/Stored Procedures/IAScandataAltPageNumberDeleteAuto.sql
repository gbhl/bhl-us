
-- IAScandataAltPageNumberDeleteAuto PROCEDURE
-- Generated 11/23/2010 11:26:17 AM
-- Do not modify the contents of this procedure.
-- Delete Procedure for IAScandataAltPageNumber

CREATE PROCEDURE IAScandataAltPageNumberDeleteAuto

@ScandataAltPageNumberID INT

AS 

DELETE FROM [dbo].[IAScandataAltPageNumber]

WHERE

	[ScandataAltPageNumberID] = @ScandataAltPageNumberID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAScandataAltPageNumberDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

