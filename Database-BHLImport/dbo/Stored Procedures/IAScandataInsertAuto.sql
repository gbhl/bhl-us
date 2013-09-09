
-- IAScandataInsertAuto PROCEDURE
-- Generated 11/24/2010 3:52:48 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for IAScandata

CREATE PROCEDURE IAScandataInsertAuto

@ScandataID INT OUTPUT,
@ItemID INT,
@Sequence INT,
@PageType NVARCHAR(50),
@PageNumber NVARCHAR(20),
@Year NVARCHAR(20) = null,
@Volume NVARCHAR(20) = null,
@Issue NVARCHAR(20) = null,
@IssuePrefix NVARCHAR(20) = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[IAScandata]
(
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
)
VALUES
(
	@ItemID,
	@Sequence,
	@PageType,
	@PageNumber,
	@Year,
	@Volume,
	@Issue,
	@IssuePrefix,
	getdate(),
	getdate()
)

SET @ScandataID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IAScandataInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

