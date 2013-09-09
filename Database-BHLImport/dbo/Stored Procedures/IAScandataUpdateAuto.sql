
-- IAScandataUpdateAuto PROCEDURE
-- Generated 11/24/2010 3:52:48 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for IAScandata

CREATE PROCEDURE IAScandataUpdateAuto

@ScandataID INT,
@ItemID INT,
@Sequence INT,
@PageType NVARCHAR(50),
@PageNumber NVARCHAR(20),
@Year NVARCHAR(20),
@Volume NVARCHAR(20),
@Issue NVARCHAR(20),
@IssuePrefix NVARCHAR(20)

AS 

SET NOCOUNT ON

UPDATE [dbo].[IAScandata]

SET

	[ItemID] = @ItemID,
	[Sequence] = @Sequence,
	[PageType] = @PageType,
	[PageNumber] = @PageNumber,
	[Year] = @Year,
	[Volume] = @Volume,
	[Issue] = @Issue,
	[IssuePrefix] = @IssuePrefix,
	[LastModifiedDate] = getdate()

WHERE
	[ScandataID] = @ScandataID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAScandataUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[ScandataID],
		[ItemID],
		[Sequence],
		[PageType],
		[PageNumber],
		[Year],
		[Volume],
		[Issue],
		[IssuePrefix],
		[CreatedDate],
		[LastModifiedDate]

	FROM [dbo].[IAScandata]
	
	WHERE
		[ScandataID] = @ScandataID
	
	RETURN -- update successful
END

