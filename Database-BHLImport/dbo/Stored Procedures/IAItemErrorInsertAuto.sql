
-- IAItemErrorInsertAuto PROCEDURE
-- Generated 11/18/2009 1:43:59 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for IAItemError

CREATE PROCEDURE IAItemErrorInsertAuto

@ItemErrorID INT OUTPUT,
@ItemID INT = null,
@ErrorDate DATETIME,
@Number INT = null,
@Severity INT = null,
@State INT = null,
@Procedure NVARCHAR(126) = null,
@Line INT = null,
@Message NVARCHAR(4000) = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[IAItemError]
(
	[ItemID],
	[ErrorDate],
	[Number],
	[Severity],
	[State],
	[Procedure],
	[Line],
	[Message]
)
VALUES
(
	@ItemID,
	@ErrorDate,
	@Number,
	@Severity,
	@State,
	@Procedure,
	@Line,
	@Message
)

SET @ItemErrorID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAItemErrorInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

