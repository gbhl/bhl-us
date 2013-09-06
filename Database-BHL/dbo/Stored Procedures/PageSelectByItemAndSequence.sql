
CREATE PROCEDURE [dbo].[PageSelectByItemAndSequence]

@ItemID INT,
@SequenceOrder INT

AS 

SET NOCOUNT ON

SELECT 

	[PageID],
	[ItemID],
	[FileNamePrefix],
	[SequenceOrder],
	[PageDescription],
	[Illustration],
	[Note],
	[FileSize_Temp],
	[FileExtension],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID],
	[Active],
	[Year],
	[Series],
	[Volume],
	[Issue],
	[ExternalURL],
	[AltExternalURL],
	[IssuePrefix],
	[LastPageNameLookupDate]

FROM [dbo].[Page]

WHERE
	[ItemID] = @ItemID AND
	[SequenceOrder] = @SequenceOrder

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageSelectByItemAndSequence. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END


