CREATE PROCEDURE [dbo].[IAMarcSelectByItem]

@ItemID INT

AS 

SET NOCOUNT ON

SELECT 

	[MarcID],
	[ItemID],
	[Leader],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[IAMarc]

WHERE
	[ItemID] = @ItemID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAMarcSelectForItem. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END



