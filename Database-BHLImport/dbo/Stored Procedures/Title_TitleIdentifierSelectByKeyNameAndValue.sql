
CREATE PROCEDURE [dbo].[Title_TitleIdentifierSelectByKeyNameAndValue]

@ImportKey NVARCHAR(50),
@IdentifierName NVARCHAR(40),
@IdentifierValue NVARCHAR(50)

AS
BEGIN

SELECT	Title_TitleIdentifierID,
		ImportKey,
		ImportStatusID,
		ImportSourceID,
		IdentifierName,
		IdentifierValue,
		ExternalCreationDate,
		ExternalLastModifiedDate,
		ProductionDate,
		CreatedDate,
		LastModifiedDate
FROM	dbo.Title_TitleIdentifier
WHERE	ImportKey = @ImportKey
AND		IdentifierName = @IdentifierName
AND		IdentifierValue = @IdentifierValue

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure Title_TitleIdentifierSelectByKeyNameAndValue. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

END
