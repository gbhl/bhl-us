CREATE PROCEDURE [dbo].[IAItemIdentifierSelect]

@ItemID INT,
@IdentifierDescription nvarchar(100),
@IdentifierValue nvarchar(125)

AS 

SET NOCOUNT ON

SELECT	[ItemIdentifierID],
		[ItemID],
		[IdentifierDescription],
		[IdentifierValue],
		[CreatedDate],
		[LastModifiedDate]
FROM	[dbo].[IAItemIdentifier]
WHERE	[ItemID] = @ItemID
AND		[IdentifierDescription] = @IdentifierDescription
AND		[IdentifierValue] = @IdentifierValue

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.IAItemIdentifierSelect. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
