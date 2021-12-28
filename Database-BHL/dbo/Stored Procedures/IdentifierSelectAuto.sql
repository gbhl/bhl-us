CREATE PROCEDURE [dbo].[IdentifierSelectAuto]

@IdentifierID INT

AS 

SET NOCOUNT ON

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
FROM	
	[dbo].[Identifier]
WHERE	
	[IdentifierID] = @IdentifierID

IF @@ERROR <> 0
BEGIN
	-- raiserror will throw a SqlException
	RAISERROR('An error occurred in procedure dbo.IdentifierSelectAuto. No information was selected.', 16, 1)
	RETURN 9 -- error occurred
END
ELSE BEGIN
	RETURN -- select successful
END

GO
