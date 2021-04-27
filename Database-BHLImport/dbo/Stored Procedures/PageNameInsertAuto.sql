CREATE PROCEDURE dbo.PageNameInsertAuto

@PageNameID INT OUTPUT,
@ImportStatusID INT,
@ImportSourceID INT = null,
@BarCode NVARCHAR(200),
@FileNamePrefix NVARCHAR(200),
@SequenceOrder INT = null,
@Source NVARCHAR(50) = null,
@NameFound NVARCHAR(100) = null,
@NameConfirmed NVARCHAR(100) = null,
@NameBankID INT = null,
@Active BIT = null,
@ExternalCreateDate DATETIME = null,
@ExternalLastUpdateDate DATETIME = null,
@IsCommonName BIT = null,
@ProductionDate DATETIME = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[PageName]
( 	[ImportStatusID],
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
	[LastModifiedDate] )
VALUES
( 	@ImportStatusID,
	@ImportSourceID,
	@BarCode,
	@FileNamePrefix,
	@SequenceOrder,
	@Source,
	@NameFound,
	@NameConfirmed,
	@NameBankID,
	@Active,
	@ExternalCreateDate,
	@ExternalLastUpdateDate,
	@IsCommonName,
	@ProductionDate,
	getdate(),
	getdate() )

SET @PageNameID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.PageNameInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END
GO
