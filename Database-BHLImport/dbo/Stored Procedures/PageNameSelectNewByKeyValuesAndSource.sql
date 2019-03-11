
CREATE PROCEDURE [dbo].[PageNameSelectNewByKeyValuesAndSource]

@BarCode NVARCHAR(40),
@FileNamePrefix NVARCHAR(200),
@NameFound NVARCHAR(100),
@ImportSourceID INT


AS 

SET NOCOUNT ON

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
	[BarCode] = @BarCode
AND	[FileNamePrefix] = @FileNamePrefix
AND	[NameFound] = @NameFound
AND [ImportSourceID] = @ImportSourceID
AND	[ImportStatusID] = 10  -- new only

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure PageNameSelectNewByKeyValuesAndSource. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
