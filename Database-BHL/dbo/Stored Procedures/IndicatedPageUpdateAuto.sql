
-- IndicatedPageUpdateAuto PROCEDURE
-- Generated 5/17/2010 4:03:17 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for IndicatedPage

CREATE PROCEDURE IndicatedPageUpdateAuto

@PageID INT,
@Sequence SMALLINT,
@PagePrefix NVARCHAR(40),
@PageNumber NVARCHAR(20),
@Implied BIT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[IndicatedPage]

SET

	[PageID] = @PageID,
	[Sequence] = @Sequence,
	[PagePrefix] = @PagePrefix,
	[PageNumber] = @PageNumber,
	[Implied] = @Implied,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[PageID] = @PageID AND
	[Sequence] = @Sequence
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IndicatedPageUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

