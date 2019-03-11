
-- IAPageInsertAuto PROCEDURE
-- Generated 12/28/2007 12:46:28 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for IAPage

CREATE PROCEDURE IAPageInsertAuto

@PageID INT OUTPUT,
@ItemID INT,
@LocalFileName NVARCHAR(200),
@Sequence INT = null,
@ExternalUrl NVARCHAR(500) = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[IAPage]
(
	[ItemID],
	[LocalFileName],
	[Sequence],
	[ExternalUrl],
	[CreatedDate],
	[LastModifiedDate]
)
VALUES
(
	@ItemID,
	@LocalFileName,
	@Sequence,
	@ExternalUrl,
	getdate(),
	getdate()
)

SET @PageID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAPageInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[PageID],
		[ItemID],
		[LocalFileName],
		[Sequence],
		[ExternalUrl],
		[CreatedDate],
		[LastModifiedDate]	

	FROM [dbo].[IAPage]
	
	WHERE
		[PageID] = @PageID
	
	RETURN -- insert successful
END

