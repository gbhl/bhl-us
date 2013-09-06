
-- PageUpdateAuto PROCEDURE
-- Generated 2/26/2008 2:23:06 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for Page

CREATE PROCEDURE PageUpdateAuto

@PageID INT,
@ItemID INT,
@FileNamePrefix NVARCHAR(50),
@SequenceOrder INT,
@PageDescription NVARCHAR(255),
@Illustration BIT,
@Note NVARCHAR(255),
@FileSize_Temp INT,
@FileExtension NVARCHAR(5),
@Active BIT,
@Year NVARCHAR(20),
@Series NVARCHAR(20),
@Volume NVARCHAR(20),
@Issue NVARCHAR(20),
@ExternalURL NVARCHAR(500),
@AltExternalURL NVARCHAR(500),
@IssuePrefix NVARCHAR(20),
@LastPageNameLookupDate DATETIME,
@PaginationUserID INT,
@PaginationDate DATETIME,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[Page]

SET

	[ItemID] = @ItemID,
	[FileNamePrefix] = @FileNamePrefix,
	[SequenceOrder] = @SequenceOrder,
	[PageDescription] = @PageDescription,
	[Illustration] = @Illustration,
	[Note] = @Note,
	[FileSize_Temp] = @FileSize_Temp,
	[FileExtension] = @FileExtension,
	[Active] = @Active,
	[Year] = @Year,
	[Series] = @Series,
	[Volume] = @Volume,
	[Issue] = @Issue,
	[ExternalURL] = @ExternalURL,
	[AltExternalURL] = @AltExternalURL,
	[IssuePrefix] = @IssuePrefix,
	[LastPageNameLookupDate] = @LastPageNameLookupDate,
	[PaginationUserID] = @PaginationUserID,
	[PaginationDate] = @PaginationDate,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID

WHERE
	[PageID] = @PageID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

