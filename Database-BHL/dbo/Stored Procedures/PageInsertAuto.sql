SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PageInsertAuto]

@PageID INT OUTPUT,
@ItemID INT = null,
@FileNamePrefix NVARCHAR(200),
@SequenceOrder INT = null,
@PageDescription NVARCHAR(255) = null,
@Illustration BIT,
@Note NVARCHAR(255) = null,
@FileSize_Temp INT = null,
@FileExtension NVARCHAR(5) = null,
@CreationUserID INT = null,
@LastModifiedUserID INT = null,
@Active BIT,
@Year NVARCHAR(20) = null,
@Series NVARCHAR(20) = null,
@Volume NVARCHAR(20) = null,
@Issue NVARCHAR(20) = null,
@ExternalURL NVARCHAR(500) = null,
@IssuePrefix NVARCHAR(20) = null,
@LastPageNameLookupDate DATETIME = null,
@PaginationUserID INT = null,
@PaginationDate DATETIME = null,
@AltExternalURL NVARCHAR(500) = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Page]
( 	[ItemID],
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
	[IssuePrefix],
	[LastPageNameLookupDate],
	[PaginationUserID],
	[PaginationDate],
	[AltExternalURL] )
VALUES
( 	@ItemID,
	@FileNamePrefix,
	@SequenceOrder,
	@PageDescription,
	@Illustration,
	@Note,
	@FileSize_Temp,
	@FileExtension,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID,
	@Active,
	@Year,
	@Series,
	@Volume,
	@Issue,
	@ExternalURL,
	@IssuePrefix,
	@LastPageNameLookupDate,
	@PaginationUserID,
	@PaginationDate,
	@AltExternalURL )

SET @PageID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.PageInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
		[IssuePrefix],
		[LastPageNameLookupDate],
		[PaginationUserID],
		[PaginationDate],
		[AltExternalURL]	
	FROM [dbo].[Page]
	WHERE
		[PageID] = @PageID
	
	RETURN -- insert successful
END


GO
