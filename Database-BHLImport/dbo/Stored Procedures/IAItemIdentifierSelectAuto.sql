CREATE PROCEDURE [dbo].[IAItemIdentifierSelectAuto]

@ItemIdentifierID INT

AS 

SET NOCOUNT ON

SELECT	
	[ItemIdentifierID],
	[ItemID],
	[IdentifierDescription],
	[IdentifierValue],
	[CreatedDate],
	[LastModifiedDate]
FROM	
	[dbo].[IAItemIdentifier]
WHERE	
	[ItemIdentifierID] = @ItemIdentifierID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.IAItemIdentifierSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
