
-- TitleVariantDeleteAuto PROCEDURE
-- Generated 2/15/2011 12:02:06 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for TitleVariant

CREATE PROCEDURE TitleVariantDeleteAuto

@TitleVariantID INT

AS 

DELETE FROM [dbo].[TitleVariant]

WHERE

	[TitleVariantID] = @TitleVariantID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleVariantDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

