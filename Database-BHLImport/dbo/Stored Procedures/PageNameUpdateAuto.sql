
-- PageNameUpdateAuto PROCEDURE
-- Generated 1/16/2008 1:54:48 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for PageName

CREATE PROCEDURE PageNameUpdateAuto

@PageNameID INT,
@ImportStatusID INT,
@ImportSourceID INT,
@BarCode NVARCHAR(40),
@FileNamePrefix NVARCHAR(50),
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
	RAISERROR('An error occurred in procedure PageNameUpdateAuto. No information was updated as a result of this request.', 16, 1)
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

