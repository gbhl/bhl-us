
-- PageInsertAuto PROCEDURE
-- Generated 2/26/2008 3:15:49 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for Page

CREATE PROCEDURE PageInsertAuto

@PageID INT OUTPUT,
@ImportStatusID INT,
@ImportSourceID INT = null,
@BarCode NVARCHAR(40),
@FileNamePrefix NVARCHAR(50),
@SequenceOrder INT = null,
@PageDescription NVARCHAR(255) = null,
@Illustration BIT = null,
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
@ExternalCreationDate DATETIME = null,
@ExternalLastModifiedDate DATETIME = null,
@ExternalCreationUser INT = null,
@ExternalLastModifiedUser INT = null,
@ProductionDate DATETIME = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Page]
(
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
	[AltExternalURL],
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
)
VALUES
(
	@ImportStatusID,
	@ImportSourceID,
	@BarCode,
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
	@ExternalCreationDate,
	@ExternalLastModifiedDate,
	@ExternalCreationUser,
	@ExternalLastModifiedUser,
	@ProductionDate,
	getdate(),
	getdate()
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
		[AltExternalURL],
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
		[PageID] = @PageID
	
	RETURN -- insert successful
END

