
-- TitleAssociationDeleteAuto PROCEDURE
-- Generated 9/4/2008 2:16:32 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for TitleAssociation

CREATE PROCEDURE TitleAssociationDeleteAuto

@TitleAssociationID INT

AS 

DELETE FROM [dbo].[TitleAssociation]

WHERE

	[TitleAssociationID] = @TitleAssociationID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleAssociationDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

