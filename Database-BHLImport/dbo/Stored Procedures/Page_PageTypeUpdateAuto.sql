CREATE PROCEDURE dbo.Page_PageTypeUpdateAuto

@PagePageTypeID INT,
@BarCode NVARCHAR(200),
@FileNamePrefix NVARCHAR(200),
@SequenceOrder INT,
@PageTypeID INT,
@ImportStatusID INT,
@ImportSourceID INT,
@Verified BIT,
@ExternalCreationDate DATETIME,
@ExternalLastModifiedDate DATETIME,
@ExternalCreationUser INT,
@ExternalLastModifiedUser INT,
@ProductionDate DATETIME

AS 

SET NOCOUNT ON

UPDATE [dbo].[Page_PageType]
SET
	[BarCode] = @BarCode,
	[FileNamePrefix] = @FileNamePrefix,
	[SequenceOrder] = @SequenceOrder,
	[PageTypeID] = @PageTypeID,
	[ImportStatusID] = @ImportStatusID,
	[ImportSourceID] = @ImportSourceID,
	[Verified] = @Verified,
	[ExternalCreationDate] = @ExternalCreationDate,
	[ExternalLastModifiedDate] = @ExternalLastModifiedDate,
	[ExternalCreationUser] = @ExternalCreationUser,
	[ExternalLastModifiedUser] = @ExternalLastModifiedUser,
	[ProductionDate] = @ProductionDate,
	[LastModifiedDate] = getdate()
WHERE
	[PagePageTypeID] = @PagePageTypeID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.Page_PageTypeUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END
GO
