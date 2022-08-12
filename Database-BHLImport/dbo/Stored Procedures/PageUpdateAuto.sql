CREATE PROCEDURE dbo.PageUpdateAuto

@PageID INT,
@ImportStatusID INT,
@ImportSourceID INT,
@BarCode NVARCHAR(200),
@FileNamePrefix NVARCHAR(200),
@SequenceOrder INT,
@PageDescription NVARCHAR(255),
@Note NVARCHAR(255),
@FileSize_Temp INT,
@FileExtension NVARCHAR(5),
@Active BIT,
@Year NVARCHAR(20),
@Series NVARCHAR(20),
@Volume NVARCHAR(20),
@Issue NVARCHAR(20),
@ExternalURL NVARCHAR(500),
@IssuePrefix NVARCHAR(20),
@LastPageNameLookupDate DATETIME,
@PaginationUserID INT,
@PaginationDate DATETIME,
@ExternalCreationDate DATETIME,
@ExternalLastModifiedDate DATETIME,
@ExternalCreationUser INT,
@ExternalLastModifiedUser INT,
@ProductionDate DATETIME,
@Illustration BIT,
@AltExternalURL NVARCHAR(500)

AS 

SET NOCOUNT ON

UPDATE [dbo].[Page]
SET
	[ImportStatusID] = @ImportStatusID,
	[ImportSourceID] = @ImportSourceID,
	[BarCode] = @BarCode,
	[FileNamePrefix] = @FileNamePrefix,
	[SequenceOrder] = @SequenceOrder,
	[PageDescription] = @PageDescription,
	[Note] = @Note,
	[FileSize_Temp] = @FileSize_Temp,
	[FileExtension] = @FileExtension,
	[Active] = @Active,
	[Year] = @Year,
	[Series] = @Series,
	[Volume] = @Volume,
	[Issue] = @Issue,
	[ExternalURL] = @ExternalURL,
	[IssuePrefix] = @IssuePrefix,
	[LastPageNameLookupDate] = @LastPageNameLookupDate,
	[PaginationUserID] = @PaginationUserID,
	[PaginationDate] = @PaginationDate,
	[ExternalCreationDate] = @ExternalCreationDate,
	[ExternalLastModifiedDate] = @ExternalLastModifiedDate,
	[ExternalCreationUser] = @ExternalCreationUser,
	[ExternalLastModifiedUser] = @ExternalLastModifiedUser,
	[ProductionDate] = @ProductionDate,
	[LastModifiedDate] = getdate(),
	[Illustration] = @Illustration,
	[AltExternalURL] = @AltExternalURL
WHERE
	[PageID] = @PageID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.PageUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[PageID],
		[ImportStatusID],
		[ImportSourceID],
		[BarCode],
		[FileNamePrefix],
		[SequenceOrder],
		[PageDescription],
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
		[LastModifiedDate],
		[Illustration],
		[AltExternalURL]
	FROM [dbo].[Page]
	WHERE
		[PageID] = @PageID
	
	RETURN -- update successful
END
GO
