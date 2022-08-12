CREATE PROCEDURE dbo.IdentifierUpdateAuto

@IdentifierID INT,
@IdentifierType NVARCHAR(40),
@IdentifierName NVARCHAR(40),
@IdentifierLabel NVARCHAR(50),
@Prefix NVARCHAR(100),
@PatternExpression NVARCHAR(200),
@PatternDescription NVARCHAR(500),
@MaximumPerEntity INT,
@Display SMALLINT,
@LastModifiedUserID INT

AS 

SET NOCOUNT ON

UPDATE [dbo].[Identifier]
SET
	[IdentifierType] = @IdentifierType,
	[IdentifierName] = @IdentifierName,
	[IdentifierLabel] = @IdentifierLabel,
	[Prefix] = @Prefix,
	[PatternExpression] = @PatternExpression,
	[PatternDescription] = @PatternDescription,
	[MaximumPerEntity] = @MaximumPerEntity,
	[Display] = @Display,
	[LastModifiedDate] = getdate(),
	[LastModifiedUserID] = @LastModifiedUserID
WHERE
	[IdentifierID] = @IdentifierID
		
IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.IdentifierUpdateAuto. No information was updated as a result of this request.', 16, 1)
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
	
	RETURN -- update successful
END

GO
