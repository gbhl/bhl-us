
-- TitleAssociation_TitleIdentifierDeleteAuto PROCEDURE
-- Generated 2/4/2011 3:01:10 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for TitleAssociation_TitleIdentifier

CREATE PROCEDURE TitleAssociation_TitleIdentifierDeleteAuto

@TitleAssociation_TitleIdentifierID INT

AS 

DELETE FROM [dbo].[TitleAssociation_TitleIdentifier]

WHERE

	[TitleAssociation_TitleIdentifierID] = @TitleAssociation_TitleIdentifierID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleAssociation_TitleIdentifierDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

