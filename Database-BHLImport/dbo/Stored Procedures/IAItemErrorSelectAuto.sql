
-- IAItemErrorSelectAuto PROCEDURE
-- Generated 11/18/2009 1:43:59 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for IAItemError

CREATE PROCEDURE IAItemErrorSelectAuto

@ItemErrorID INT

AS 

SET NOCOUNT ON

SELECT 

	[ItemErrorID],
	[ItemID],
	[ErrorDate],
	[Number],
	[Severity],
	[State],
	[Procedure],
	[Line],
	[Message]

FROM [dbo].[IAItemError]

WHERE
	[ItemErrorID] = @ItemErrorID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAItemErrorSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

