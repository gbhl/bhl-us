
-- Title_TitleIdentifierSelectAuto PROCEDURE
-- Generated 9/4/2008 2:16:32 PM
-- Do not modify the contents of this procedure.
-- Select Procedure for Title_TitleIdentifier

CREATE PROCEDURE Title_TitleIdentifierSelectAuto

@Title_TitleIdentifierID INT

AS 

SET NOCOUNT ON

SELECT 

	[Title_TitleIdentifierID],
	[ImportKey],
	[ImportStatusID],
	[ImportSourceID],
	[IdentifierName],
	[IdentifierValue],
	[ExternalCreationDate],
	[ExternalLastModifiedDate],
	[ProductionDate],
	[CreatedDate],
	[LastModifiedDate]

FROM [dbo].[Title_TitleIdentifier]

WHERE
	[Title_TitleIdentifierID] = @Title_TitleIdentifierID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure Title_TitleIdentifierSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

