
-- IADCMetadataUpdateAuto PROCEDURE
-- Generated 12/28/2007 12:46:28 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for IADCMetadata

CREATE PROCEDURE IADCMetadataUpdateAuto

@DCMetadataID INT,
@ItemID INT,
@DCElementName NVARCHAR(15),
@DCElementValue NVARCHAR(500),
@Source NVARCHAR(50)

AS 

SET NOCOUNT ON

UPDATE [dbo].[IADCMetadata]

SET

	[ItemID] = @ItemID,
	[DCElementName] = @DCElementName,
	[DCElementValue] = @DCElementValue,
	[Source] = @Source,
	[LastModifiedDate] = getdate()

WHERE
	[DCMetadataID] = @DCMetadataID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IADCMetadataUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- update successful
END

