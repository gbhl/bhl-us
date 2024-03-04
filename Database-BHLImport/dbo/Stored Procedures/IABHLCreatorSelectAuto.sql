CREATE PROCEDURE [dbo].[IABHLCreatorSelectAuto]

@BHLCreatorID INT

AS 

SET NOCOUNT ON

SELECT	
	[BHLCreatorID],
	[ItemID],
	[Name],
	[CreatedDate],
	[LastModifiedDate]
FROM	
	[dbo].[IABHLCreator]
WHERE	
	[BHLCreatorID] = @BHLCreatorID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.IABHLCreatorSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
