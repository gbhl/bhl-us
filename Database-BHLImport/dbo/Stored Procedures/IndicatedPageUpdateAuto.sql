CREATE PROCEDURE dbo.IndicatedPageUpdateAuto

@IndicatedPageID INT,
@BarCode NVARCHAR(200),
@FileNamePrefix NVARCHAR(200),
@SequenceOrder INT,
@Sequence SMALLINT,
@ImportStatusID INT,
@ImportSourceID INT,
@PagePrefix NVARCHAR(40),
@PageNumber NVARCHAR(20),
@Implied BIT,
@ExternalCreationDate DATETIME,
@ExternalLastModifiedDate DATETIME,
@ExternalCreationUser INT,
@ExternalLastModifiedUser INT,
@ProductionDate DATETIME

AS 

SET NOCOUNT ON

UPDATE [dbo].[IndicatedPage]
SET
	[BarCode] = @BarCode,
	[FileNamePrefix] = @FileNamePrefix,
	[SequenceOrder] = @SequenceOrder,
	[Sequence] = @Sequence,
	[ImportStatusID] = @ImportStatusID,
	[ImportSourceID] = @ImportSourceID,
	[PagePrefix] = @PagePrefix,
	[PageNumber] = @PageNumber,
	[Implied] = @Implied,
	[ExternalCreationDate] = @ExternalCreationDate,
	[ExternalLastModifiedDate] = @ExternalLastModifiedDate,
	[ExternalCreationUser] = @ExternalCreationUser,
	[ExternalLastModifiedUser] = @ExternalLastModifiedUser,
	[ProductionDate] = @ProductionDate,
	[LastModifiedDate] = getdate()
WHERE
	[IndicatedPageID] = @IndicatedPageID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.IndicatedPageUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[IndicatedPageID],
		[BarCode],
		[FileNamePrefix],
		[SequenceOrder],
		[Sequence],
		[ImportStatusID],
		[ImportSourceID],
		[PagePrefix],
		[PageNumber],
		[Implied],
		[ExternalCreationDate],
		[ExternalLastModifiedDate],
		[ExternalCreationUser],
		[ExternalLastModifiedUser],
		[ProductionDate],
		[CreatedDate],
		[LastModifiedDate]
	FROM [dbo].[IndicatedPage]
	WHERE
		[IndicatedPageID] = @IndicatedPageID
	
	RETURN -- update successful
END
GO
