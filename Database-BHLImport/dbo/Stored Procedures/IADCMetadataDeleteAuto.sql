
-- IADCMetadataDeleteAuto PROCEDURE
-- Generated 12/28/2007 12:46:28 PM
-- Do not modify the contents of this procedure.
-- Delete Procedure for IADCMetadata

CREATE PROCEDURE IADCMetadataDeleteAuto

@DCMetadataID INT

AS 

DELETE FROM [dbo].[IADCMetadata]

WHERE

	[DCMetadataID] = @DCMetadataID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IADCMetadataDeleteAuto. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

