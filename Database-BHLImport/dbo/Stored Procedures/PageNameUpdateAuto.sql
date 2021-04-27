CREATE PROCEDURE dbo.PageNameUpdateAuto

@PageNameID INT,
@ImportStatusID INT,
@ImportSourceID INT,
@BarCode NVARCHAR(200),
@FileNamePrefix NVARCHAR(200),
@SequenceOrder INT,
@Source NVARCHAR(50),
@NameFound NVARCHAR(100),
@NameConfirmed NVARCHAR(100),
@NameBankID INT,
@Active BIT,
@ExternalCreateDate DATETIME,
@ExternalLastUpdateDate DATETIME,
@IsCommonName BIT,
@ProductionDate DATETIME

AS 

SET NOCOUNT ON

UPDATE [dbo].[PageName]
SET
	[ImportStatusID] = @ImportStatusID,
	[ImportSourceID] = @ImportSourceID,
	[BarCode] = @BarCode,
	[FileNamePrefix] = @FileNamePrefix,
	[SequenceOrder] = @SequenceOrder,
	[Source] = @Source,
	[NameFound] = @NameFound,
	[NameConfirmed] = @NameConfirmed,
	[NameBankID] = @NameBankID,
	[Active] = @Active,
	[ExternalCreateDate] = @ExternalCreateDate,
	[ExternalLastUpdateDate] = @ExternalLastUpdateDate,
	[IsCommonName] = @IsCommonName,
	[ProductionDate] = @ProductionDate,
	[LastModifiedDate] = getdate()
WHERE
	[PageNameID] = @PageNameID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.PageNameUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[PageNameID],
		[ImportStatusID],
		[ImportSourceID],
		[BarCode],
		[FileNamePrefix],
		[SequenceOrder],
		[Source],
		[NameFound],
		[NameConfirmed],
		[NameBankID],
		[Active],
		[ExternalCreateDate],
		[ExternalLastUpdateDate],
		[IsCommonName],
		[ProductionDate],
		[CreatedDate],
		[LastModifiedDate]
	FROM [dbo].[PageName]
	WHERE
		[PageNameID] = @PageNameID
	
	RETURN -- update successful
END
GO
