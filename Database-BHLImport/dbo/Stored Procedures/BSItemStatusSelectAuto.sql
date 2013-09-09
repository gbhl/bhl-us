
-- BSItemStatusSelectAuto PROCEDURE
-- Generated 10/23/2012 3:24:24 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for BSItemStatus

CREATE PROCEDURE BSItemStatusSelectAuto

@ItemStatusID INT

AS 

SET NOCOUNT ON

SELECT 

	[ItemStatusID],
	[Status],
	[Description],
	[CreationDate],
	[LastModifiedDate]

FROM [dbo].[BSItemStatus]

WHERE
	[ItemStatusID] = @ItemStatusID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure BSItemStatusSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

