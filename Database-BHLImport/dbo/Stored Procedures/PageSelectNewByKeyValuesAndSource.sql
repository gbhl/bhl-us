
CREATE PROCEDURE [dbo].[PageSelectNewByKeyValuesAndSource]

@BarCode NVARCHAR(40),
@FileNamePrefix NVARCHAR(50),
@ImportSourceID INT

AS 

SET NOCOUNT ON

SELECT 

	[PageID],
	[ImportStatusID],
	[ImportSourceID],
	[BarCode],
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
	[IssuePrefix],
	[LastPageNameLookupDate],
	[PaginationUserID],
	[PaginationDate],
	[ExternalCreationDate],
	[ExternalLastModifiedDate],
	[ExternalCreationUser],
	[ExternalLastModifiedUser],
	[ProductionDate],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[Page]

WHERE
	[BarCode] = @BarCode
AND	[FileNamePrefix] = @FileNamePrefix
AND [ImportSourceID] = @ImportSourceID
AND	[ImportStatusID] = 10  -- new only

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageSelectNewByKeyValuesAndSource. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
