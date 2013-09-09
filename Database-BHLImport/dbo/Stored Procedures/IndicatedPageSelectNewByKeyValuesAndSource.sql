
CREATE PROCEDURE [dbo].[IndicatedPageSelectNewByKeyValuesAndSource]

@BarCode NVARCHAR(40),
@FileNamePrefix NVARCHAR(50),
@Sequence SMALLINT,
@ImportSourceID INT


AS 

SET NOCOUNT ON

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
	[BarCode] = @BarCode
AND	[FileNamePrefix] = @FileNamePrefix
AND	[Sequence] = @Sequence
AND [ImportSourceID] = @ImportSourceID
AND	[ImportStatusID] = 10  -- new only

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure IndicatedPageSelectNewByKeyValuesAndSource. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

