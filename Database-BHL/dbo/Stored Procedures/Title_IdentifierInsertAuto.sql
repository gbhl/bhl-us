
-- Title_IdentifierInsertAuto PROCEDURE
-- Generated 5/1/2012 2:41:41 PM
-- Do not modify the contents of this procedure.
-- Insert Procedure for Title_Identifier

CREATE PROCEDURE Title_IdentifierInsertAuto

@TitleIdentifierID INT OUTPUT,
@TitleID INT,
@IdentifierID INT,
@IdentifierValue NVARCHAR(125),
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Title_Identifier]
(
	[TitleID],
	[IdentifierID],
	[IdentifierValue],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID]
)
VALUES
(
	@TitleID,
	@IdentifierID,
	@IdentifierValue,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID
)

SET @TitleIdentifierID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure Title_IdentifierInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
	
		[TitleIdentifierID],
		[TitleID],
		[IdentifierID],
		[IdentifierValue],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	

	FROM [dbo].[Title_Identifier]
	
	WHERE
		[TitleIdentifierID] = @TitleIdentifierID
	
	RETURN -- insert successful
END


