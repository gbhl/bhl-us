
-- TitleAssociationTypeDeleteAuto PROCEDURE
-- Generated 5/6/2009 2:57:04 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for TitleAssociationType

CREATE PROCEDURE TitleAssociationTypeDeleteAuto

@TitleAssociationTypeID INT

AS 

DELETE FROM [dbo].[TitleAssociationType]

WHERE

	[TitleAssociationTypeID] = @TitleAssociationTypeID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleAssociationTypeDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

