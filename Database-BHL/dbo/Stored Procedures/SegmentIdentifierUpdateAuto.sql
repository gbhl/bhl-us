
-- SegmentIdentifierUpdateAuto PROCEDURE
-- Generated 9/18/2012 12:12:30 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for SegmentIdentifier

CREATE PROCEDURE SegmentIdentifierUpdateAuto

@SegmentIdentifierID INT,
@SegmentID INT,
@IdentifierID INT,
@IdentifierValue NVARCHAR(125),
@IsContainerIdentifier SMALLINT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[SegmentIdentifier]

SET

	[SegmentID] = @SegmentID,
	[IdentifierID] = @IdentifierID,
	[IdentifierValue] = @IdentifierValue,
	[IsContainerIdentifier] = @IsContainerIdentifier,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[SegmentIdentifierID] = @SegmentIdentifierID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentIdentifierUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

