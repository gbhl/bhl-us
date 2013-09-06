
-- IndicatedPageInsertAuto PROCEDURE
-- Generated 5/17/2010 4:03:17 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for IndicatedPage

CREATE PROCEDURE IndicatedPageInsertAuto

@PageID INT,
@Sequence SMALLINT,
@PagePrefix NVARCHAR(40) = null,
@PageNumber NVARCHAR(20) = null,
@Implied BIT,
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

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
	RAISERROR('An error occurred in procedure IndicatedPageInsertAuto. No information was inserted as a result of this request.', 16, 1)
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

