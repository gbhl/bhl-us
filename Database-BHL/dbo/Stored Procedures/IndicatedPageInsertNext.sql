
CREATE PROCEDURE [dbo].[IndicatedPageInsertNext]

@PageID INT /* Unique identifier for each Page record. */,
@PagePrefix NVARCHAR(40) = null /* Prefix portion of Indicated Page. */,
@PageNumber NVARCHAR(20) = null /* Page Number portion of Indicated Page. */,
@Implied BIT,
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

DECLARE @Sequence SMALLINT /* A number to separately identify various series of Indicated Pages. */

SELECT @Sequence = MAX(Sequence) 
FROM [dbo].[IndicatedPage]
WHERE PageID = @PageID

IF (@Sequence IS NULL) SELECT @Sequence = 1
ELSE SELECT @Sequence = @Sequence + 1

INSERT INTO [dbo].[IndicatedPage]
(
	[PageID],
	[Sequence],
	[PagePrefix],
	[PageNumber],
	[Implied],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@PageID,
	@Sequence,
	@PagePrefix,
	@PageNumber,
	@Implied,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IndicatedPageInsertNext. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[PageID],
		[Sequence],
		[PagePrefix],
		[PageNumber],
		[Implied],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]		

	FROM [dbo].[IndicatedPage]
	
	WHERE
		[PageID] = @PageID AND
		[Sequence] = @Sequence
	
	RETURN -- insert successful
END

