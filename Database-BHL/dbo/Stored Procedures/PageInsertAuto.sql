
-- PageInsertAuto PROCEDURE
-- Generated 2/26/2008 2:23:06 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for Page

CREATE PROCEDURE PageInsertAuto

@PageID INT OUTPUT,
@ItemID INT,
@FileNamePrefix NVARCHAR(200),
@SequenceOrder INT = null,
@PageDescription NVARCHAR(255) = null,
@Illustration BIT,
@Note NVARCHAR(255) = null,
@FileSize_Temp INT = null,
@FileExtension NVARCHAR(5) = null,
@Active BIT,
@Year NVARCHAR(20) = null,
@Series NVARCHAR(20) = null,
@Volume NVARCHAR(20) = null,
@Issue NVARCHAR(20) = null,
@ExternalURL NVARCHAR(500) = null,
@AltExternalURL NVARCHAR(500) = null,
@IssuePrefix NVARCHAR(20) = null,
@LastPageNameLookupDate DATETIME = null,
@PaginationUserID INT = null,
@PaginationDate DATETIME = null,
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Page]
(
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
)
VALUES
(
	@ItemID,
	@FileNamePrefix,
	@SequenceOrder,
	@PageDescription,
	@Illustration,
	@Note,
	@FileSize_Temp,
	@FileExtension,
	@Active,
	@Year,
	@Series,
	@Volume,
	@Issue,
	@ExternalURL,
	@AltExternalURL,
	@IssuePrefix,
	@LastPageNameLookupDate,
	@PaginationUserID,
	@PaginationDate,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

SET @PageID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

