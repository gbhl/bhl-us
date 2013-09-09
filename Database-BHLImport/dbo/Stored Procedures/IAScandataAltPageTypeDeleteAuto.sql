
-- IAScandataAltPageTypeDeleteAuto PROCEDURE
-- Generated 11/23/2010 11:26:17 AM
-- Do not modify the contents of this procedure.
-- Delete Procedure for IAScandataAltPageType

CREATE PROCEDURE IAScandataAltPageTypeDeleteAuto

@ScandataAltPageTypeID INT

AS 

DELETE FROM [dbo].[IAScandataAltPageType]

WHERE

	[ScandataAltPageTypeID] = @ScandataAltPageTypeID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAScandataAltPageTypeDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

