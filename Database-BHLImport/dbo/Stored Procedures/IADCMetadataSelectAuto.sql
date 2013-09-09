
-- IADCMetadataSelectAuto PROCEDURE
-- Generated 12/28/2007 12:46:28 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for IADCMetadata

CREATE PROCEDURE IADCMetadataSelectAuto

@DCMetadataID INT

AS 

SET NOCOUNT ON

SELECT 

	[DCMetadataID],
	[ItemID],
	[DCElementName],
	[DCElementValue],
	[Source],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[IADCMetadata]

WHERE
	[DCMetadataID] = @DCMetadataID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IADCMetadataSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

