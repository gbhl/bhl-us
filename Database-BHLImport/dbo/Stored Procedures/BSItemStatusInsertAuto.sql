
-- BSItemStatusInsertAuto PROCEDURE
-- Generated 10/23/2012 3:24:24 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for BSItemStatus

CREATE PROCEDURE BSItemStatusInsertAuto

@ItemStatusID INT,
@Status NVARCHAR(30),
@Description NVARCHAR(4000)

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[BSItemStatus]
(
	[ItemStatusID],
	[Status],
	[Description],
	[CreationDate],
	[LastModifiedDate]
)
VALUES
(
	@ItemStatusID,
	@Status,
	@Description,
	getdate(),
	getdate()
)

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure BSItemStatusInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[ItemStatusID],
		[Status],
		[Description],
		[CreationDate],
		[LastModifiedDate]	

	FROM [dbo].[BSItemStatus]
	
	WHERE
		[ItemStatusID] = @ItemStatusID
	
	RETURN -- insert successful
END

