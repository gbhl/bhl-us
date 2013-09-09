
-- BSItemInsertAuto PROCEDURE
-- Generated 10/23/2012 3:54:50 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for BSItem

CREATE PROCEDURE BSItemInsertAuto

@ItemID INT OUTPUT,
@BHLItemID INT = null,
@ItemStatusID INT

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[BSItem]
(
	[BHLItemID],
	[ItemStatusID],
	[CreationDate],
	[LastModifiedDate]
)
VALUES
(
	@BHLItemID,
	@ItemStatusID,
	getdate(),
	getdate()
)

SET @ItemID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure BSItemInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[ItemID],
		[BHLItemID],
		[ItemStatusID],
		[CreationDate],
		[LastModifiedDate]	

	FROM [dbo].[BSItem]
	
	WHERE
		[ItemID] = @ItemID
	
	RETURN -- insert successful
END

