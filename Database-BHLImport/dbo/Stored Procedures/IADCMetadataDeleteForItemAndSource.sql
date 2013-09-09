CREATE PROCEDURE [dbo].[IADCMetadataDeleteForItemAndSource]

@ItemID INT,
@Source NVARCHAR(10)

AS 

DELETE FROM [dbo].[IADCMetadata]

WHERE

	[ItemID] = @ItemID
AND	[Source] = @Source

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IADCMetadataDeleteForItemAndSource. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END


