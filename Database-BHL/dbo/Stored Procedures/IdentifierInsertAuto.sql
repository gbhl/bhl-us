CREATE PROCEDURE dbo.IdentifierInsertAuto

@IdentifierID INT OUTPUT,
@IdentifierType NVARCHAR(40),
@IdentifierName NVARCHAR(40),
@IdentifierLabel NVARCHAR(50),
@Prefix NVARCHAR(100),
@PatternExpression NVARCHAR(200),
@PatternDescription NVARCHAR(500),
@MaximumPerEntity INT,
@Display SMALLINT,
@CreationUserID INT = null,
@LastModifiedUserID INT = null

AS 

SET NOCOUNT ON

INSERT INTO [dbo].[Identifier]
( 	[IdentifierType],
	[IdentifierName],
	[IdentifierLabel],
	[Prefix],
	[PatternExpression],
	[PatternDescription],
	[MaximumPerEntity],
	[Display],
	[CreationDate],
	[LastModifiedDate],
	[CreationUserID],
	[LastModifiedUserID] )
VALUES
( 	@IdentifierType,
	@IdentifierName,
	@IdentifierLabel,
	@Prefix,
	@PatternExpression,
	@PatternDescription,
	@MaximumPerEntity,
	@Display,
	getdate(),
	getdate(),
	@CreationUserID,
	@LastModifiedUserID )

SET @IdentifierID = Scope_Identity()

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.IdentifierInsertAuto. No information was inserted as a result of this request.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	SELECT
		[IdentifierID],
		[IdentifierType],
		[IdentifierName],
		[IdentifierLabel],
		[Prefix],
		[PatternExpression],
		[PatternDescription],
		[MaximumPerEntity],
		[Display],
		[CreationDate],
		[LastModifiedDate],
		[CreationUserID],
		[LastModifiedUserID]	
	FROM [dbo].[Identifier]
	WHERE
		[IdentifierID] = @IdentifierID
	
	RETURN -- insert successful
END

GO
