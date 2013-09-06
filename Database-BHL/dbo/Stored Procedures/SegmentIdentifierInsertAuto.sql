
-- SegmentIdentifierInsertAuto PROCEDURE
-- Generated 9/18/2012 12:12:30 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for SegmentIdentifier

CREATE PROCEDURE SegmentIdentifierInsertAuto

@SegmentIdentifierID INT OUTPUT,
@SegmentID INT,
@IdentifierID INT,
@IdentifierValue NVARCHAR(125),
@IsContainerIdentifier SMALLINT = null,
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[SegmentIdentifier]
(
	[SegmentID],
	[IdentifierID],
	[IdentifierValue],
	[IsContainerIdentifier],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@SegmentID,
	@IdentifierID,
	@IdentifierValue,
	@IsContainerIdentifier,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

SET @SegmentIdentifierID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentIdentifierInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[SegmentIdentifierID],
		[SegmentID],
		[IdentifierID],
		[IdentifierValue],
		[IsContainerIdentifier],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	

	FROM [dbo].[SegmentIdentifier]
	
	WHERE
		[SegmentIdentifierID] = @SegmentIdentifierID
	
	RETURN -- insert successful
END

