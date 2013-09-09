
-- IAItemSetInsertAuto PROCEDURE
-- Generated 12/28/2007 12:46:28 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for IAItemSet

CREATE PROCEDURE IAItemSetInsertAuto

@ItemID INT,
@SetID INT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[IAItemSet]
(
	[ItemID],
	[SetID],
	[CreatedDate]
)
VALUES
(
	@ItemID,
	@SetID,
	getdate()
)

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAItemSetInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[ItemID],
		[SetID],
		[CreatedDate]	

	FROM [dbo].[IAItemSet]
	
	WHERE
		[ItemID] = @ItemID AND
		[SetID] = @SetID
	
	RETURN -- insert successful
END

