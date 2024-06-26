﻿CREATE PROCEDURE [dbo].[IABHLCreatorDeleteByItem]

@ItemID INT

AS

DELETE 
FROM	dbo.IABHLCreatorIdentifier
WHERE	BHLCreatorID IN (	SELECT	BHLCreatorID
							FROM	dbo.IABHLCreator
							WHERE	ItemID = @ItemID )
							

DELETE 
FROM	dbo.IABHLCreator
WHERE	ItemID = @ItemID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IABHLCreatorDeleteByItem. No information was deleted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred 
END
ELSE BEGIN
	RETURN 0 -- delete successful
END

GO
