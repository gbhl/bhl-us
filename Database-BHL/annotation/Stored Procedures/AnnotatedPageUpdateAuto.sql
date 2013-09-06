
-- AnnotatedPageUpdateAuto PROCEDURE
-- Generated 7/14/2010 1:25:28 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for AnnotatedPage

CREATE PROCEDURE [annotation].AnnotatedPageUpdateAuto

@AnnotatedPageID INT,
@AnnotatedItemID INT,
@PageID INT,
@ExternalIdentifier NVARCHAR(50),
@AnnotatedPageTypeID INT,
@PageNumber NVARCHAR(20)

AS 

SET NOCOUNT ON

UPDATE [annotation].[AnnotatedPage]

SET

	[AnnotatedItemID] = @AnnotatedItemID,
	[PageID] = @PageID,
	[ExternalIdentifier] = @ExternalIdentifier,
	[AnnotatedPageTypeID] = @AnnotatedPageTypeID,
	[PageNumber] = @PageNumber,
	[LastModifiedDate] = getdate()

WHERE
	[AnnotatedPageID] = @AnnotatedPageID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotatedPageUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[AnnotatedPageID],
		[AnnotatedItemID],
		[PageID],
		[ExternalIdentifier],
		[AnnotatedPageTypeID],
		[PageNumber],
		[CreationDate],
		[LastModifiedDate]

	FROM [annotation].[AnnotatedPage]
	
	WHERE
		[AnnotatedPageID] = @AnnotatedPageID
	
	RETURN -- update successful
END

