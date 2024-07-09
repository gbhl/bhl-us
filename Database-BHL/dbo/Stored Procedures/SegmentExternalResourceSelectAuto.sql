CREATE PROCEDURE [dbo].[SegmentExternalResourceSelectAuto]

@SegmentExternalResourceID INT

AS 

SET NOCOUNT ON

SELECT	
	[SegmentExternalResourceID],
	[SegmentID],
	[ExternalResourceTypeID],
	[UrlText],
	[Url],
	[SequenceOrder],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
FROM	
	[dbo].[SegmentExternalResource]
WHERE	
	[SegmentExternalResourceID] = @SegmentExternalResourceID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.SegmentExternalResourceSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
