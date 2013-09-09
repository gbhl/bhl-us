
CREATE PROCEDURE [dbo].[TitleTagSelectNewByKeyTagTextAndSource]

@ImportKey NVARCHAR(50),
@TagText NVARCHAR(50),
@ImportSourceID INT

AS 

SET NOCOUNT ON

SELECT 

	[TitleTagID],
	[TagText],
	[ImportKey],
	[ImportStatusID],
	[ImportSourceID],
	[MarcDataFieldTag],
	[MarcSubFieldCode],
	[ExternalCreationDate],
	[ExternalLastModifiedDate],
	[ProductionDate],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[TitleTag]

WHERE
	[ImportKey] = @ImportKey
AND [TagText] = @TagText
AND	[ImportSourceID] = @ImportSourceID
AND	[ImportStatusID] = 10 -- new only

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure TitleTagSelectNewByKeyTagTextAndSource. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END
