
-- Title_TitleIdentifierUpdateAuto PROCEDURE
-- Generated 9/4/2008 2:16:32 PM
-- Do not modify the contents of this procedure.
-- Update Procedure for Title_TitleIdentifier

CREATE PROCEDURE Title_TitleIdentifierUpdateAuto

@Title_TitleIdentifierID INT,
@ImportKey NVARCHAR(50),
@ImportStatusID INT,
@ImportSourceID INT,
@IdentifierName NVARCHAR(40),
@IdentifierValue NVARCHAR(125),
@ExternalCreationDate DATETIME,
@ExternalLastModifiedDate DATETIME,
@ProductionDate DATETIME

AS 

SET NOCOUNT ON

UPDATE [dbo].[Title_TitleIdentifier]

SET

	[ImportKey] = @ImportKey,
	[ImportStatusID] = @ImportStatusID,
	[ImportSourceID] = @ImportSourceID,
	[IdentifierName] = @IdentifierName,
	[IdentifierValue] = @IdentifierValue,
	[ExternalCreationDate] = @ExternalCreationDate,
	[ExternalLastModifiedDate] = @ExternalLastModifiedDate,
	[ProductionDate] = @ProductionDate,
	[LastModifiedDate] = getdate()

WHERE
	[Title_TitleIdentifierID] = @Title_TitleIdentifierID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure Title_TitleIdentifierUpdateAuto. No information was updated as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
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
	
	RETURN -- update successful
END

