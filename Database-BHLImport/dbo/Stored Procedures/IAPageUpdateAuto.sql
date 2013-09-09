
-- IAPageUpdateAuto PROCEDURE
-- Generated 12/28/2007 12:46:28 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for IAPage

CREATE PROCEDURE IAPageUpdateAuto

@PageID INT,
@ItemID INT,
@LocalFileName NVARCHAR(50),
@Sequence INT,
@ExternalUrl NVARCHAR(500)

AS 

SET NOCOUNT ON

UPDATE [dbo].[IAPage]

SET

	[ItemID] = @ItemID,
	[LocalFileName] = @LocalFileName,
	[Sequence] = @Sequence,
	[ExternalUrl] = @ExternalUrl,
	[LastModifiedDate] = getdate()

WHERE
	[PageID] = @PageID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAPageUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

