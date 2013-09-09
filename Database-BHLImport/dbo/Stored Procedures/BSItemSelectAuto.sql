
-- BSItemSelectAuto PROCEDURE
-- Generated 10/23/2012 3:54:50 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for BSItem

CREATE PROCEDURE BSItemSelectAuto

@ItemID INT

AS 

SET NOCOUNT ON

SELECT 

	[ItemID],
	[BHLItemID],
	[ItemStatusID],
	[CreationDate],
	[LastModifiedDate]

FROM [dbo].[BSItem]

WHERE
	[ItemID] = @ItemID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure BSItemSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

