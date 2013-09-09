
CREATE PROCEDURE [dbo].[Page_PageTypeSelectNewByKeyValuesAndSource]

@BarCode NVARCHAR(40),
@FileNamePrefix NVARCHAR(50),
@PageTypeID INT,
@ImportSourceID INT

AS 

SET NOCOUNT ON

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
	[BarCode] = @BarCode
AND	[FileNamePrefix] = @FileNamePrefix
AND	[PageTypeID] = @PageTypeID
AND [ImportSourceID] = @ImportSourceID
AND	[ImportStatusID] = 10  -- new only

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure Page_PageTypeSelectNewByKeyValuesAndSource. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

