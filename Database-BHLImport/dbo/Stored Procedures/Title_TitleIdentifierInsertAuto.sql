
-- Title_TitleIdentifierInsertAuto PROCEDURE
-- Generated 9/4/2008 2:16:32 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for Title_TitleIdentifier

CREATE PROCEDURE Title_TitleIdentifierInsertAuto

@Title_TitleIdentifierID INT OUTPUT,
@ImportKey NVARCHAR(50),
@ImportStatusID INT,
@ImportSourceID INT = null,
@IdentifierName NVARCHAR(40),
@IdentifierValue NVARCHAR(125),
@ExternalCreationDate DATETIME = null,
@ExternalLastModifiedDate DATETIME = null,
@ProductionDate DATETIME = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Title_TitleIdentifier]
(
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
)
VALUES
(
	@ImportKey,
	@ImportStatusID,
	@ImportSourceID,
	@IdentifierName,
	@IdentifierValue,
	@ExternalCreationDate,
	@ExternalLastModifiedDate,
	@ProductionDate,
	getdate(),
	getdate()
)

SET @Title_TitleIdentifierID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure Title_TitleIdentifierInsertAuto. No information was inserted as a result of this request.', 16, 1)
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
	
	RETURN -- insert successful
END

