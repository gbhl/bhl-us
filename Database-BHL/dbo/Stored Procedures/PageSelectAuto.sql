
-- PageSelectAuto PROCEDURE
-- Generated 2/26/2008 2:23:06 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for Page

CREATE PROCEDURE PageSelectAuto

@PageID INT

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
	[Active],
	[Year],
	[Series],
	[Volume],
	[Issue],
	[ExternalURL],
	[AltExternalURL],
	[IssuePrefix],
	[LastPageNameLookupDate],
	[PaginationUserID],
	[PaginationDate],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]

FROM [dbo].[Page]

WHERE
	[PageID] = @PageID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

