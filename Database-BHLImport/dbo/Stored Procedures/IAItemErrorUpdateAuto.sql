
-- IAItemErrorUpdateAuto PROCEDURE
-- Generated 11/18/2009 1:43:59 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for IAItemError

CREATE PROCEDURE IAItemErrorUpdateAuto

@ItemErrorID INT,
@ItemID INT,
@ErrorDate DATETIME,
@Number INT,
@Severity INT,
@State INT,
@Procedure NVARCHAR(126),
@Line INT,
@Message NVARCHAR(4000)

AS 

SET NOCOUNT ON

UPDATE [dbo].[IAItemError]

SET

	[ItemID] = @ItemID,
	[ErrorDate] = @ErrorDate,
	[Number] = @Number,
	[Severity] = @Severity,
	[State] = @State,
	[Procedure] = @Procedure,
	[Line] = @Line,
	[Message] = @Message

WHERE
	[ItemErrorID] = @ItemErrorID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAItemErrorUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- update successful
END

