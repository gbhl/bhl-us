
-- SegmentIdentifierSelectAuto PROCEDURE
-- Generated 9/18/2012 12:12:30 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for SegmentIdentifier

CREATE PROCEDURE SegmentIdentifierSelectAuto

@SegmentIdentifierID INT

AS 

SET NOCOUNT ON

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

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure SegmentIdentifierSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

