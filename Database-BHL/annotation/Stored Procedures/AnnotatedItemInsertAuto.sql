
-- AnnotatedItemInsertAuto PROCEDURE
-- Generated 7/14/2010 1:25:28 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for AnnotatedItem

CREATE PROCEDURE [annotation].AnnotatedItemInsertAuto

@AnnotatedItemID INT OUTPUT,
@AnnotatedTitleID INT,
@ItemID INT = null,
@ExternalIdentifier NVARCHAR(50),
@Volume NVARCHAR(10)

AS 

SET NOCOUNT ON

INSERT INTO [annotation].[AnnotatedItem]
(
	[AnnotatedTitleID],
	[ItemID],
	[ExternalIdentifier],
	[Volume],
	[CreationDate],
	[LastModifiedDate]
)
VALUES
(
	@AnnotatedTitleID,
	@ItemID,
	@ExternalIdentifier,
	@Volume,
	getdate(),
	getdate()
)

SET @AnnotatedItemID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotatedItemInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

