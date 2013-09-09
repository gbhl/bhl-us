
-- IADCMetadataInsertAuto PROCEDURE
-- Generated 12/28/2007 12:46:28 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for IADCMetadata

CREATE PROCEDURE IADCMetadataInsertAuto

@DCMetadataID INT OUTPUT,
@ItemID INT,
@DCElementName NVARCHAR(15),
@DCElementValue NVARCHAR(500),
@Source NVARCHAR(50)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[IADCMetadata]
(
	[ItemID],
	[DCElementName],
	[DCElementValue],
	[Source],
	[CreatedDate],
	[LastModifiedDate]
)
VALUES
(
	@ItemID,
	@DCElementName,
	@DCElementValue,
	@Source,
	getdate(),
	getdate()
)

SET @DCMetadataID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IADCMetadataInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

