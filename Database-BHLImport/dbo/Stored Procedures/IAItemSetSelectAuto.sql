
-- IAItemSetSelectAuto PROCEDURE
-- Generated 12/28/2007 12:46:28 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for IAItemSet

CREATE PROCEDURE IAItemSetSelectAuto

@ItemID INT,
@SetID INT

AS 

SET NOCOUNT ON

SELECT 

	[ItemID],
	[SetID],
	[CreatedDate]

FROM [dbo].[IAItemSet]

WHERE
	[ItemID] = @ItemID AND
	[SetID] = @SetID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAItemSetSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

