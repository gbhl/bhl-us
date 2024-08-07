CREATE PROCEDURE dbo.Page_PageTypeInsertAuto

@PagePageTypeID INT OUTPUT,
@BarCode NVARCHAR(200),
@FileNamePrefix NVARCHAR(200),
@SequenceOrder INT = null,
@PageTypeID INT,
@ImportStatusID INT,
@ImportSourceID INT = null,
@Verified BIT,
@ExternalCreationDate DATETIME = null,
@ExternalLastModifiedDate DATETIME = null,
@ExternalCreationUser INT = null,
@ExternalLastModifiedUser INT = null,
@ProductionDate DATETIME = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Page_PageType]
( 	[BarCode],
	[FileNamePrefix],
	[SequenceOrder],
	[PageTypeID],
	[ImportStatusID],
	[ImportSourceID],
	[Verified],
	[ExternalCreationDate],
	[ExternalLastModifiedDate],
	[ExternalCreationUser],
	[ExternalLastModifiedUser],
	[ProductionDate],
	[CreatedDate],
	[LastModifiedDate] )
VALUES
( 	@BarCode,
	@FileNamePrefix,
	@SequenceOrder,
	@PageTypeID,
	@ImportStatusID,
	@ImportSourceID,
	@Verified,
	@ExternalCreationDate,
	@ExternalLastModifiedDate,
	@ExternalCreationUser,
	@ExternalLastModifiedUser,
	@ProductionDate,
	getdate(),
	getdate() )

SET @PagePageTypeID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.Page_PageTypeInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[PagePageTypeID],
		[BarCode],
		[FileNamePrefix],
		[SequenceOrder],
		[PageTypeID],
		[ImportStatusID],
		[ImportSourceID],
		[Verified],
		[ExternalCreationDate],
		[ExternalLastModifiedDate],
		[ExternalCreationUser],
		[ExternalLastModifiedUser],
		[ProductionDate],
		[CreatedDate],
		[LastModifiedDate]	
	FROM [dbo].[Page_PageType]
	WHERE
		[PagePageTypeID] = @PagePageTypeID
	
	RETURN -- insert successful
END
GO
