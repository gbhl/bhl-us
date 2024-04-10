CREATE PROCEDURE [dbo].[IABHLCreatorIdentifierSelectAuto]

@BHLCreatorIdentifierID INT

AS 

SET NOCOUNT ON

SELECT	
	[BHLCreatorIdentifierID],
	[BHLCreatorID],
	[Type],
	[Value],
	[CreatedDate],
	[LastModifiedDate]
FROM	
	[dbo].[IABHLCreatorIdentifier]
WHERE	
	[BHLCreatorIdentifierID] = @BHLCreatorIdentifierID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.IABHLCreatorIdentifierSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
