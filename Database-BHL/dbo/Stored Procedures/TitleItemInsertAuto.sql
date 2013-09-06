
-- TitleItemInsertAuto PROCEDURE
-- Generated 2/4/2011 3:25:21 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for TitleItem

CREATE PROCEDURE TitleItemInsertAuto

@TitleItemID INT OUTPUT,
@TitleID INT,
@ItemID INT,
@ItemSequence SMALLINT = null,
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[TitleItem]
(
	[TitleID],
	[ItemID],
	[ItemSequence],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@TitleID,
	@ItemID,
	@ItemSequence,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

SET @TitleItemID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleItemInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[TitleItemID],
		[TitleID],
		[ItemID],
		[ItemSequence],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	

	FROM [dbo].[TitleItem]
	
	WHERE
		[TitleItemID] = @TitleItemID
	
	RETURN -- insert successful
END

