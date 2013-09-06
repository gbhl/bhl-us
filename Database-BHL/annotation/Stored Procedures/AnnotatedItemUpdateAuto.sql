
-- AnnotatedItemUpdateAuto PROCEDURE
-- Generated 7/14/2010 1:25:28 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for AnnotatedItem

CREATE PROCEDURE [annotation].AnnotatedItemUpdateAuto

@AnnotatedItemID INT,
@AnnotatedTitleID INT,
@ItemID INT,
@ExternalIdentifier NVARCHAR(50),
@Volume NVARCHAR(10)

AS 

SET NOCOUNT ON

UPDATE [annotation].[AnnotatedItem]

SET

	[AnnotatedTitleID] = @AnnotatedTitleID,
	[ItemID] = @ItemID,
	[ExternalIdentifier] = @ExternalIdentifier,
	[Volume] = @Volume,
	[LastModifiedDate] = getdate()

WHERE
	[AnnotatedItemID] = @AnnotatedItemID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotatedItemUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[AnnotatedItemID],
		[AnnotatedTitleID],
		[ItemID],
		[ExternalIdentifier],
		[Volume],
		[CreationDate],
		[LastModifiedDate]

	FROM [annotation].[AnnotatedItem]
	
	WHERE
		[AnnotatedItemID] = @AnnotatedItemID
	
	RETURN -- update successful
END

