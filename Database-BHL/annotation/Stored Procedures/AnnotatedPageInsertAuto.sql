
-- AnnotatedPageInsertAuto PROCEDURE
-- Generated 7/14/2010 1:25:28 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for AnnotatedPage

CREATE PROCEDURE [annotation].AnnotatedPageInsertAuto

@AnnotatedPageID INT OUTPUT,
@AnnotatedItemID INT,
@PageID INT = null,
@ExternalIdentifier NVARCHAR(50),
@AnnotatedPageTypeID INT,
@PageNumber NVARCHAR(20)

AS 

SET NOCOUNT ON

INSERT INTO [annotation].[AnnotatedPage]
(
	[AnnotatedItemID],
	[PageID],
	[ExternalIdentifier],
	[AnnotatedPageTypeID],
	[PageNumber],
	[CreationDate],
	[LastModifiedDate]
)
VALUES
(
	@AnnotatedItemID,
	@PageID,
	@ExternalIdentifier,
	@AnnotatedPageTypeID,
	@PageNumber,
	getdate(),
	getdate()
)

SET @AnnotatedPageID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure AnnotatedPageInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

